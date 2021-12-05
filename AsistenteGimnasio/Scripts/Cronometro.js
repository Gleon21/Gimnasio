////var stopWatchInterval = null;
////var stopWatchStart = null;
////var stopWatchValue = null;
////var stopWatchDisplay = null;

////function pad(n, width, z) {
////    z = z || '0';
////    n = n + '';
////    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
////}

////function UpdateStopWatch(newDate) {
////    stopWatchValue = newDate - stopWatchStart;
////    var seconds = Math.floor(stopWatchValue / 1000);
////    var hours = Math.floor(seconds / 3600);
////    var minutes = Math.floor((seconds % 3600) / 60);
////    seconds = seconds % 60;
////    stopWatchDisplay.val(pad(hours, 2) + ':' + pad(minutes, 2) + ':' + pad(seconds, 2));
////}

////$(function () {
////    stopWatchDisplay = $('#txtStopWatch');
////    $('#start').click(function () {
////        stopWatchStart = new Date();
////        UpdateStopWatch(stopWatchStart);
////        stopWatchInterval = setInterval(
////            function () {
////                var now = new Date();
////                UpdateStopWatch(now);
////            }, 50);
////    });

////    $('#stop').click(function () {
////        clearInterval(stopWatchInterval);
////        stopWatchValue = (new Date()) - stopWatchStart;
////    });
////});

//window.onload = function () {

//    visor = document.getElementById("reloj"); //localizar pantalla del reloj
//    //asociar eventos a botones: al pulsar el botón se activa su función.
//    document.cron.boton1.onclick = activo;
//    document.cron.boton2.onclick = pausa;
//    document.cron.boton1.disabled = false;
//    document.cron.boton2.disabled = true;
//    //variables de inicio:
//    var marcha = 0; //control del temporizador
//    var cro = 0; //estado inicial del cronómetro.

//}

////botón Empezar / Reiniciar
//function activo() {
//    if (document.cron.boton1.value == "Empezar") { //botón en "Empezar"
//        empezar() //ir  la función empezar
//    }
//    else {  //Botón en "Reiniciar"
//        reiniciar()  //ir a la función reiniciar
//    }
//}
////botón pausa / continuar
//function pausa() {
//    if (marcha == 0) { //con el boton en "continuar"
//        continuar() //ir a la función continuar
//    }
//    else {  //con el botón en "parar"
//        parar() //ir a la funcion parar
//    }
//}
////Botón 1: Estado empezar: Poner en marcha el crono
//function empezar() {
//    emp = new Date() //fecha inicial (en el momento de pulsar)
//    elcrono = setInterval(tiempo, 10); //función del temporizador.
//    marcha = 1 //indicamos que se ha puesto en marcha.
//    document.cron.boton1.value = "Reiniciar"; //Cambiar estado del botón
//    document.cron.boton2.disabled = false; //activar botón de pausa
//}
////función del temporizador			
//function tiempo() {
//    actual = new Date(); //fecha a cada instante
//    //tiempo del crono (cro) = fecha instante (actual) - fecha inicial (emp)
//    cro = actual - emp; //milisegundos transcurridos.
//    cr = new Date(); //pasamos el num. de milisegundos a objeto fecha.
//    cr.setTime(cro);
//    //obtener los distintos formatos de la fecha:
//    cs = cr.getMilliseconds(); //milisegundos 
//    cs = cs / 10; //paso a centésimas de segundo.
//    cs = Math.round(cs); //redondear las centésimas
//    sg = cr.getSeconds(); //segundos 
//    mn = cr.getMinutes(); //minutos 
//    ho = cr.getHours() - 1; //horas 
//    //poner siempre 2 cifras en los números		 
//    if (cs < 10) { cs = "0" + cs; }
//    if (sg < 10) { sg = "0" + sg; }
//    if (mn < 10) { mn = "0" + mn; }
//    //llevar resultado al visor.		 
//    visor.innerHTML = ho + " " + mn + " " + sg + " " + cs;
//}
////parar el cronómetro
//function parar() {
//    clearInterval(elcrono); //parar el crono
//    marcha = 0; //indicar que está parado.
//    document.cron.boton2.value = "Continuar"; //cambiar el estado del botón
//}
////Continuar una cuenta empezada y parada.
//function continuar() {
//    emp2 = new Date(); //fecha actual
//    emp2 = emp2.getTime(); //pasar a tiempo Unix
//    emp3 = emp2 - cro; //restar tiempo anterior
//    emp = new Date(); //nueva fecha inicial para pasar al temporizador 
//    emp.setTime(emp3); //datos para nueva fecha inicial.
//    elcrono = setInterval(tiempo, 10); //activar temporizador
//    marcha = 1; //indicar que esta en marcha
//    document.cron.boton2.value = "parar"; //Cambiar estado del botón
//    document.cron.boton1.disabled = false; //activar boton 1 si estuviera desactivado
//}
////Volver al estado inicial
//function reiniciar() {
//    if (marcha == 1) {  //si el cronómetro está en marcha:
//        clearInterval(elcrono); //parar el crono
//        marcha = 0;	//indicar que está parado
//    }
//    //en cualquier caso volvemos a los valores iniciales
//    cro = 0; //tiempo transcurrido a cero
//    visor.innerHTML = "0 00 00 00"; //visor a cero
//    document.cron.boton1.value = "Empezar"; //estado inicial primer botón
//    document.cron.boton2.value = "Parar"; //estado inicial segundo botón
//    document.cron.boton2.disabled = true;  //segundo botón desactivado	 
//}	

//window.onload = function () {
//    pantalla = document.getElementById("screen");
//}
//var isMarch = false;
//var acumularTime = 0;
//function start() {
//    if (isMarch == false) {
//        timeInicial = new Date();
//        control = setInterval(cronometro, 10);
//        isMarch = true;
//    }
//}
//function cronometro() {
//    timeActual = new Date();
//    acumularTime = timeActual - timeInicial;
//    acumularTime2 = new Date();
//    acumularTime2.setTime(acumularTime);
//    cc = Math.round(acumularTime2.getMilliseconds() / 10);
//    ss = acumularTime2.getSeconds();
//    mm = acumularTime2.getMinutes();
//    hh = acumularTime2.getHours() - 18;
//    if (cc < 10) { cc = "0" + cc; }
//    if (ss < 10) { ss = "0" + ss; }
//    if (mm < 10) { mm = "0" + mm; }
//    if (hh < 10) { hh = "0" + hh; }
//    pantalla.innerHTML = hh + " : " + mm + " : " + ss + " : " + cc;
//}

//function stop() {
//    if (isMarch == true) {
//        clearInterval(control);
//        isMarch = false;
//    }
//}

//function resume() {
//    if (isMarch == false) {
//        timeActu2 = new Date();
//        timeActu2 = timeActu2.getTime();
//        acumularResume = timeActu2 - acumularTime;

//        timeInicial.setTime(acumularResume);
//        control = setInterval(cronometro, 10);
//        isMarch = true;
//    }
//}

//function reset() {
//    if (isMarch == true) {
//        clearInterval(control);
//        isMarch = false;
//    }
//    acumularTime = 0;
//    pantalla.innerHTML = "00 : 00 : 00 : 00";
//}

window.onload = init;
function init() {
    document.querySelector(".start").addEventListener("click", cronometrar);
    document.querySelector(".stop").addEventListener("click", parar);
    document.querySelector(".reiniciar").addEventListener("click", reiniciar);
    h = 0;
    m = 0;
    s = 0;
    document.getElementById("hms").innerHTML = "00:00:00";
}
function cronometrar() {
    escribir();
    id = setInterval(escribir, 1000);
    document.querySelector(".start").removeEventListener("click", cronometrar);
}
function escribir() {
    var hAux, mAux, sAux;
    s++;
    if (s > 59) { m++; s = 0; }
    if (m > 59) { h++; m = 0; }
    if (h > 24) { h = 0; }

    if (s < 10) { sAux = "0" + s; } else { sAux = s; }
    if (m < 10) { mAux = "0" + m; } else { mAux = m; }
    if (h < 10) { hAux = "0" + h; } else { hAux = h; }

    document.getElementById("hms").innerHTML = hAux + ":" + mAux + ":" + sAux;
}
function parar() {
    clearInterval(id);
    document.querySelector(".start").addEventListener("click", cronometrar);

}
function reiniciar() {
    clearInterval(id);
    document.getElementById("hms").innerHTML = "00:00:00";
    h = 0; m = 0; s = 0;
    document.querySelector(".start").addEventListener("click", cronometrar);
}