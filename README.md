# OutfitControl
Controle de uniformes

## 0. Atenção inicial
- O comando `docker` no Linux podem ser necessários ser executados como superusuário
- Os comandos `docker` e `dotnet` no Windows precisam estar no path

## 1. Dependências para build:
### SDK do .NET 10

Para Windows, baixe o SDK site oficial da Microsoft:

https://dotnet.microsoft.com/download/dotnet/10.0

---

Para Linux, utilize os pacotes oficiais da distribuição:

Arch
```sh
sudo pacman -S dotnet-sdk-10.0
```
### Docker

No Windows, baixe o docker para Windows:

https://docs.docker.com/desktop/setup/install/windows-install/

---

No Linux, utilize os pacotes oficial da distribuição

```sh
sudo pacman -S docker docker-compose docker-buildx
```
## 2. Build

Na raiz do projeto execute

```sh
dotnet publish /t:PublishContainer -c Release
```

Verifique se a imagem do container foi criada:

No Windows, utilize a interface do Docker Desktop se existe a imagem **outfitcontrol**

No Linux use o comando:
```sh
docker image ls
```

Se a imagem docker estiver criada, pode seguir para o passo 3

## 3. Revisando o compose.yaml e executando

O projeto usa como banco de dados o MariaDB, que é essencial para o projeto. Uma instância MariaDB é necessária na porta 3306 para o OutfitControl executar.

Existe 2 scripts de inicialização do banco na pasta `init`. O arquivo `01_schema.sql` inicializa o esquema de banco de dados, enquanto o arquivo `02_populate.sql` popula o banco com dados __fantasia__ de funcionários.
A aplicação não possui uma forma de inserir dados de funcionários por ela, já que isso sairia do escopo da solução

Para executar estes scripts, precisa-se montar a pasta `init` no entrypoint do container do banco de dados. Assim na primeira execução do container o banco será formado e terá os dados populados.

Exemplo:
```yaml
services:
  db:
    image: mariadb
    # ...
    volumes:
      - ./init:/docker-entrypoint-initdb.d
    # ...
```

A aplicação OutfitControl por padrão executa na porta 8080 dentro do container. Para executar na porta 80 da máquina host, garanta que a porta está corretamente mapeada:

```yaml
services:
  outfitcontrol:
    image: outfitcontrol
    # ...
    ports:
      - 80:8080
    # ...
```

O arquivo `compose.yaml` do repositório já está preparado e pronto para ser realizado os testes.

Para subir e executar o container:

```sh
docker compose up -d
```
