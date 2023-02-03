# RabbitMqSchedulerPoc

POC para implementação da utilização do RabbitMQ com o Masstransit operando com scheduler na entrega das mensagens.

A solution possui 3 projetos:
  - Consumer: Recebe as mensagens do RabbitMQ e utiliza o Hangfire para agendar a entrega destas mensagens ao cosumidor final.
  - Woker: Responsável pelo envio das mensagens ao RabbitMQ.
  - Shared: Onde ficam os objetos que representam as mensagens.
 
 Para executar o projeto é necessário ter o RabbitMQ rodando na máquina.
