# ms-weather-music

## Descrição do que foi desenvolvido no Projeto
O projeto **ms-weather-music** é uma aplicação de microsserviços que combina informações sobre clima e música. O nome reflete a funcionalidade central do serviço e padroniza o prefixo para microsserviços. 

## Estrutura de Pastas
A arquitetura do projeto foi organizada em diferentes pastas, cada uma com responsabilidades específicas:

### 1. Controllers
A pasta **Controllers** é responsável pela lógica de entrada da aplicação, processando solicitações do usuário e interagindo com os modelos. Entre os controladores, destaca-se o **WeatherMusicController**, que disponibiliza rotas para buscar músicas com base em condições climáticas e localização:
- `api/v1/weather-music/top-musics/city/{cityName}`: Recebe o nome da cidade como parâmetro. Para ficar mais preciso coloque o nome da cidade, vírgula, código do país com 2 letras (ISO3166). Você obterá todas as cidades adequadas no país escolhido. A ordem é importante - o primeiro é o nome da cidade, depois a vírgula e depois o país. Exemplo - Londres, GB ou Nova York, EUA.
- `api/v1/weather-music/top-music/coordinates/{latitude}/{longitude}`: Recebe latitude e longitude como parâmetros.

### 2. Models
Na pasta **Models**, encontramos os objetos de retorno das chamadas das APIs internas e de serviços externos. Os principais arquivos incluem:
- **AuthorizeResponse**: Resposta de autorização.
- **OpenWeatherMapResponse**: Respostas de API do serviço OpenWeatherMap.
- **SpotifyResponse**: Respostas de API do Spotify.
- **WeatherMusicResponse**: Respostas específicas para as rotas do controlador correspondente.

### 3. Services
A pasta **Services** contém a lógica de negócios que orquestra as interações entre modelos e controladores. As subpastas incluem:
- **Authorizer**: Serviços de autorização. Esse serviço funciona como um simulador de autorização. Assim, em caso de receber uma recusa, basta executá-lo novamente até que uma autorização seja concedida.
- **OpenWeatherMap**: Interação com a API do OpenWeatherMap.
- **Spotify**: Interação com a API do Spotify. Nessa service foi integrado um processo de cache de tokens com o objetivo de mitigar o impacto na performance das requisições.
- **WeatherMusic**: Funções relacionadas à lógica do controlador WeatherMusicController.

### 4. Settings
A pasta **Settings** abriga configurações de ambiente, mantendo variáveis sensíveis fora do código-fonte. Inclui arquivos para serviços de autorização, OpenWeatherMap e Spotify.

### 5. Utils
Na pasta **Utils**, encontram-se utilitários para validação e listagem de opções. Exemplos incluem **CityNameValidator** e **CoordinatesValidator**, que evitam requisições desnecessárias.

### 6. Testes
O projeto de testes unitários, denominado **ms-weather-music.Tests**, abrange testes para a pasta Controllers e Services, com foco no **AuthorizerService** e **WeatherMusicController**.

### 7. Solution
O arquivo de solução, **caiapo-challenge.sln**, referencia todos os projetos da solução, facilitando a organização.

## Execução do Projeto
### Localmente
- **Iniciar serviço**: Entre na pasta principal(ms-weather-music) e execute o comando `dotnet run`
- **URL Base**: `http://localhost:5226`
- **Swagger**: `http://localhost:5226/swagger`

### Via Docker
- **Baixar Imagem**: `docker pull lucasmrassis/ms_weather_music:latest`
- **Iniciar Container**: `docker run -d -p 8000:80 lucasmrassis/ms_weather_music`
- **URL Base**: `http://localhost:8000`
- **Swagger**: `http://localhost:8000/swagger`

## Vantagens da Estrutura de Pastas
A abordagem utilizada na divisão de pastas no projeto **ms-weather-music** é vantajosa por várias razões:

1. **Clareza e Manutenção**: Cada pasta e componente possui uma responsabilidade clara, o que facilita a compreensão do fluxo da aplicação e a manutenção do código. Isso contrasta com uma estrutura monolítica onde várias responsabilidades podem estar amontoadas em arquivos únicos, dificultando a identificação de problemas e a realização de alterações.

2. **Reutilização e Testabilidade**: A separação em serviços e modelos permite a reutilização de componentes e a realização de testes específicos sem interferir em outras partes do código. Nos sistemas monolíticos, os testes podem se tornar complexos e difíceis de gerenciar.

3. **Escalabilidade**: Uma estrutura modular permite que novos recursos sejam adicionados com menos impacto sobre a base existente, enquanto no modelo monolítico, qualquer nova funcionalidade pode exigir alterações extensivas.

4. **Colaboração**: Múltiplos desenvolvedores podem trabalhar em diferentes partes do sistema simultaneamente sem causar conflitos, uma vez que cada área é claramente definida.

Em comparação a outras abordagens — como a ausência de uma estrutura organizacional, que pode levar a um código desordenado — a estrutura adotada neste projeto garante robustez, facilidade na manutenção e na expansão do sistema.

# Informações sobre o desafio

# Sobre a Caiapó
A construtora Caiapó é uma das maiores construtoras de infraestrutura do Brasil com obras espalhadas em todo território nacional.
Visando processos cada vez mais maduros, eficientes, e com sistemas robustos que ofereçam cada vez mais suporte ao modelo de negócio, desde 2019 a Caiapó vem investindo no desenvolvimento de suas próprias soluções de software.

# O Desafio
Crie um microsserviço capaz de aceitar solicitações RESTful que recebam como parâmetro o nome da cidade ou as coordenadas (*latitude e longitude*) e retorne as 5 tops músicas (*apenas os nomes das músicas*) de um determinado artista de acordo com a temperatura atual.

## Requisitos
1) Se a temperatura(*celsius*) estiver acima de 30 graus, entregue as 5 top músicas do David Guetta;
2) Caso a temperatura esteja entre 15 e 30 graus, entregue as 5 top músicas da Madonna;
3) Se estiver um pouco frio (entre 10 e 14 graus), entregue as 5 top de Os Paralamas do Sucesso;
4) Caso contrário, se estiver frio lá fora, entregue as 5 top músicas de Ludwig van Beethoven.

## Requisitos não funcionais
- O microsserviço deve estar preparado para ser tolerante a falhas, responsivo e resiliente;
- Utilize a linguagem C# .NET. Use qualquer ferramenta e estrutura com as quais se sinta confortável e elabore brevemente sua solução, detalhes de arquitetura, escolha de padrões e estruturas;
- Crie uma imagem docker do seu microsserviço e publique em algum hub a sua escolha (exemplo: dockerhub) e informe o comando para que seu serviço possa ser implantado localmente.

## Dicas
Você pode usar a API do *[OpenWeatherMaps](https://openweathermap.org)* para buscar dados de temperatura e o *[Spotify](https://developer.spotify.com/)* para sugerir as músicas da playlist.

A interpretação dos requisitos e o fornecimento da melhor solução são fundamentais para atender bem aos clientes desse microsserviço. Dê o seu melhor! :)

## Recomendações
- Faça um fork desse repositório para subir a sua solução e podermos avaliar seu código;
- Utilize padrões de documentação de commit e Pull Request;
- Utilize C#;
- Utilize .NET 8;
- Utilize docker;
- Utilize boas práticas de codificação, isso será avaliado;
- Mostre que conhece c#;
- Código limpo, organizado e documentado (quando necesário);
- Use e abuse de:
    - SOLID;
    - Criatividade;
    - Performance;
    - Manutenabilidade;
    - Testes de Unidade (quando necessário);
    - ... pois avaliaremos tudo !
