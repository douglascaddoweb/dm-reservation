# Projeto para reseva de motocicletas e entregas de pedidos

Primeiro passo para rodar o projeto deverá ser criado um banco de dados postgres e configurado a conexão no arquivo appsettings dos projetos
DMReservation.API e DMReservation.Consumer.Worker

`
    {
        ...
        "ConnectionStrings": {
            "Default": <informações de conexão da base de dados>
        },
    }
`

Após a configuração rodar o comando `Update-Database` dentro do projeto DMReservation.Infra para criar as tabelas do projeto via migrations.

Nos projetos DMReservation.API e DMReservation.Consumer.Worker também será necessário configurar os acessos do RabbitMq para envio de mensagens.

No projeto DMReservation.Consumer.Worker existe a configuração para informar o canal que deverá ser escutado para as mensagens.
portanto para poder consumir as filas deverá ser alterado o valor final com o id da tabela deliveryman informando assim qual entregador
recebera a mensagem.


`
    {
        ...
        "Rabbit":
        {
            ...
            "Channel": "DMReservation.Message.DeliveryMan.<iddeliveryman>"
        }
    }
`
