using OutfitControl.Database.Repositories;
using OutfitControl.Entities;
using OutfitControl.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace OutfitControl.Database;

public class OutfitControlService
{
    private FuncionarioRepository  _funcionarioRepository;
    private LoteRepository _loteRepository;
    private PecaPorPedidoRepository _pecaPorPedidoRepository;
    private PecaRepository _pecaRepository;
    private PedidoRepository _pedidoRepository;
    private RetiradaRepository _retiradaRepository;

    public OutfitControlService(FuncionarioRepository funcionarioRepository,
        LoteRepository loteRepository,
        PecaPorPedidoRepository pecaPorPedidoRepository,
        PecaRepository pecaRepository,
        PedidoRepository pedidoRepository,
        RetiradaRepository retriadaRepository)
    {
        _funcionarioRepository = funcionarioRepository;
        _loteRepository = loteRepository;
        _pecaPorPedidoRepository = pecaPorPedidoRepository;
        _pecaRepository = pecaRepository;
        _pedidoRepository = pedidoRepository;
        _retiradaRepository = retriadaRepository;
    }

    public void SolicitarUniformeDeFuncionario(Funcionario? funcionario, IList<TipoPeca> tipoPecas, IList<string> tamanhoPecas, int quantidadePecas)
    {
        if (funcionario == null)
            throw new ArgumentNullException(nameof(funcionario));
        if(tipoPecas == null)
            throw new ArgumentNullException(nameof(tipoPecas));
        if (tamanhoPecas == null)
            throw new ArgumentNullException(nameof(tamanhoPecas));
        if (tipoPecas.Count != tamanhoPecas.Count)
            throw new ArgumentException("Quantidade de tipos e de tamanhos de peça não são iguais");
        if (tipoPecas.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(tipoPecas));
        
        var pecas = new List<Peca>();
        var pecasPorPedido = new List<PecaPorPedido>();

        for (int indice = 0; indice < tipoPecas.Count; ++indice)
            pecas.Add(_pecaRepository.GetOrAddByTipoETamanho(tipoPecas[indice], tamanhoPecas[indice]));

        var pedido = new Pedido
        {
            Funcionario = funcionario,
            Data = DateOnly.FromDateTime(DateTime.Now),
            Status = StatusPedido.Novo
        };
        
        foreach (var peca in pecas)
            pecasPorPedido.Add(new PecaPorPedido
            {
                Peca = peca,
                Pedido = pedido,
                Quantidade = quantidadePecas
            });
        
        _pedidoRepository.Add(pedido);
        
        foreach (var pecaPorPedido in pecasPorPedido)
            _pecaPorPedidoRepository.Add(pecaPorPedido);
    }

    public void SolicitarFabricacaoUniforme(IList<Pedido> pedidos)
    {
        foreach (var pedido in pedidos)
        {
            pedido.Status = StatusPedido.EmProcesso;
            _pedidoRepository.Update(pedido);
        }
    }

    public Lote RegistrarLote(TipoPeca tipoPeca, string tamanhoPeca, int quantidadeRecebida)
    {
        var peca = _pecaRepository.GetOrAddByTipoETamanho(tipoPeca, tamanhoPeca);
        var lote = new Lote
        {
            Peca = peca,
            Quantidade = quantidadeRecebida,
            Data = DateOnly.FromDateTime(DateTime.Now)
        };
        _loteRepository.Add(lote);

        return lote;
    }

    /// <summary>
    /// Envia o pedido para aguardar retirada e desconta as peças do estoque (lotes).
    /// </summary>
    public Retirada EnviarParaRetirada(Pedido pedido)
    {
        // Desconta do estoque cada peça do pedido
        foreach (var pp in pedido.Pecas)
        {
            var restante = pp.Quantidade;

            // Busca os lotes disponíveis desta peça, do mais antigo para o mais novo
            var lotes = _loteRepository.GetAllComDetalhes()
                .Where(l => l.Peca.Tipo == pp.Peca.Tipo && l.Peca.Tamanho == pp.Peca.Tamanho && l.Quantidade > 0)
                .OrderBy(l => l.Data)
                .ToList();

            foreach (var lote in lotes)
            {
                if (restante <= 0) break;

                if (lote.Quantidade >= restante)
                {
                    lote.Quantidade -= restante;
                    restante = 0;
                }
                else
                {
                    restante -= lote.Quantidade;
                    lote.Quantidade = 0;
                }

                _loteRepository.Update(lote);
            }
        }

        pedido.Status = StatusPedido.AguardandoRetirada;
        _pedidoRepository.Update(pedido);

        var retirada = new Retirada
        {
            Pedido = pedido,
            Data = DateOnly.FromDateTime(DateTime.Now)
        };

        return _retiradaRepository.Add(retirada);
    }

    /// <summary>
    /// Confirma que o funcionário retirou fisicamente o uniforme. Finaliza o pedido.
    /// </summary>
    public void FinalizarRetirada(Pedido pedido)
    {
        pedido.Status = StatusPedido.Finalizado;
        _pedidoRepository.Update(pedido);
    }

    // Mantido por compatibilidade — use EnviarParaRetirada + FinalizarRetirada no novo fluxo
    public Retirada RetirarUniforme(Pedido pedido)
    {
        return EnviarParaRetirada(pedido);
    }
}
