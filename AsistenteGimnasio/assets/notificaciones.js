
    //<script src="~/Scripts/jquery.signalR-2.4.1.js"></script>
    //    <script src="/signalr/hubs"></script>
      

        
            $(function () {

                var chat = $.connection.notificacionesHub;


                chat.client.sendnotificacion = function () {



                //var divmessage = $("<div />").text(message).html();

                $("#Notifi").append("                <a class='dropdown-item' type='submit' href='#')'>" /*+ message +*/ + " prueba " + "</a>");

            };

            //var nameOwner = prompt("Escribe tu nombre:", "");
            //$("#displayname").val(nameOwner);

            //$("#message").focus();

                $.connection.hub.start().done(function () {
                $("#sendmessage").click(function () {



                    chat.server.send("hola");



                })
            })

        })




