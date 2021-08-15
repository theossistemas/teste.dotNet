export function DateMask(evento) {
	/*	Modo de usar:
		TextBox_Data.Attributes.Add("onpaste", "return false;")
		TextBox_Data.Attributes.Add("onKeyPress", "FormataData(this.name,event);") 
		TextBox_Data.Attributes.Add("onBlur", "FormataData(this.name,event);") */
	var evt;
	if(evento!=null){
		evt = evento;
	}else{
		evt = evento;
	}	 
    var charCode = (document.all)? evt.keyCode : evt.which ;
    
	var tammax = 8;
	var Numero = true;
	var vr= evento.target.value.replace(/[^0-9]+/g,'');;
	var tam = vr.length;
	var valorCampo = '';
	if(evento.target.value !=null){
		valorCampo = evento.target.value;
	}else{
		//campo = campo.replace(/\:/g, '_'); //como o que chega vem pelo atributo name, trocar os ':' pelo '_'
		
	}
	
	var vetor = valorCampo.split("/");
	if (tam >= tammax){ 
		evt.cancelBubble = true; evt.returnValue = false;
	}
	else {
		if (vetor.length == 3){
			if(vetor[0].length == 1) { vetor[0] = '0' + vetor[0]; }
			if(vetor[1].length == 1) { vetor[1] = '0' + vetor[1]; }
			vr = vetor[0] + '' + vetor[1] + '' + vetor[2];
			evento.target.value = vetor[0] + '/' + vetor[1] + '/' + vetor[2];
			if (vetor[2].length == 4) {
				evt.cancelBubble = true; evt.returnValue = false;
			}
		}
	}
	tam = vr.length + Numero;
  var tecla1 = String.fromCharCode(evt.keyCode); 
  var tecla = parseInt(tecla1);
	
	var dia = 0, mes = 0, diasNoMes = 31, ano = 0;
	//Se a tecla digitada n�o for num�rica (um tab ou enter por exemplo)
	if (!Numero) { 	
		if ( tam > 0 ) {
			if ( tam < 8) {
				//document.Form1[campo].focus();
				evento.target.value = evento.target.value;			
				return false;
			} else {
				if ( !valida_data(evento.target.value) ) {
					//document.Form1[campo].focus();
					evento.target.value = '';		
					return false;
				}	
			}
		}		
	//Se a tecla digitada for num�rica
	} else { 
		if ( (tam == 1) && (tecla > 3) ) { 
			//o dia n�o pode iniciar com n�mero maior do que 3 ou seja n�o existe dia 40 ou superior
			evt.cancelBubble = true; evt.returnValue = false; 
		}
		else {
			if ( tam == 2 ) {
				dia = parseInt(vr.substr( 0, 1 ));
				if ( (dia == 0 && tecla == 0) || (dia == 3 && tecla > 1) ) {
					//o dia n�o pode ser 00 nem maior do que 31
					evt.cancelBubble = true; evt.returnValue = false;
				} 
			}
			else {
				if ( tam == 3 ) {	
					dia = parseInt(vr.substr( 0, 2 )); 
					if ( tecla > 1 ) {
						//o m�s n�o pode iniciar com n�mero maior do que 1 ou seja n�o existe m�s 20 ou superior
						evt.cancelBubble = true; evt.returnValue = false;
					}
				}
				else {
					if ( tam == 4 ) {
						mes = parseInt(vr.substr( 2, 1 ));
						if ( (mes == 1 && tecla > 2) || (mes == 0 && tecla == 0) ) {
							//o m�s n�o pode ser maior do que 12 e nem 00
							evt.cancelBubble = true; evt.returnValue = false; 
						} else {
							dia = parseInt(vr.substr( 0, 2 ));
							mes = parseInt(vr.substr( 2, 1 ) + tecla);
							if ( mes == 2 ) {
								//a quantidade de dias do m�s de fevereiro pode ser no m�ximo 29 (em anos bissextos)
								diasNoMes = 29; 
							} else {
								if ( mes == 4 || mes == 6 || mes == 9 || mes == 11) {
									diasNoMes = 30;
								}
							}
							if ( dia > diasNoMes ) {
								//altera o dia se a quantidade de dias digitada for maior do que a quantidade de dias permitida para o m�s
								evento.target.value = diasNoMes + '/' + vr.substr( 2, 1 );
							}
						}
					}
					else {
						if ( tam == 5 ) {
							if ( !(tecla == 1 || tecla == 2) ) {
								//o ano n�o pode iniciar com digitos diferentes de 1 ou 2
								evt.cancelBubble = true; evt.returnValue = false;  }
						}
						else {
							if ( tam == 6) {
								ano = parseInt(vr.substr( 4, 1 ));
								if ( ano == 1 && tecla < 8  ) {
									//o ano n�o pde ser menor do que 1800
									evt.cancelBubble = true; evt.returnValue = false;
								}
							}
							else {
								if ( tam == 8 ) {						
									dia = parseInt(vr.substr( 0, 2 ));
									mes = parseInt(vr.substr( 2, 2 ));
									ano = parseInt(vr.substr( 4, 3 ) + tecla );								
									if ( (mes == 2 && dia == 29) && (ano / 4 != (ano / 4)) ) {
										//altera o dia para 28 se o ano digitado for 29 e o ano n�o for bissexto
										diasNoMes = 28; dia = diasNoMes; vr = diasNoMes + '' + vr.substr( 2, 2 ) + vr.substr( 4, 3 );
										evento.target.value = diasNoMes + '/' + vr.substr( 2, 2 ) + '/' + vr.substr( 4, 3 );										
									}
								}
							}
						}
					}
				}
			}
		}
  }

  if ( tam == 3 ) {
    evento.target.value = vr +  '/';
  } else {
    if ( tam == 5 ) {
      evento.target.value = vr.substr( 0, 2 ) + '/' + vr.substr( 2, 2 ) + '/';
    } else {
      if ( tam == 8 ) {
        evento.target.value = vr.substr( 0, 2 ) + '/' + vr.substr( 2, 2 ) + '/' + vr.substr( 4, 4 );
      }
    }
  }
}

export function valida_data(valor){ 
 
  var data= valor;
  var posbarra1, posbarra2, dia, mes, ano;
  if (data==""){
    return false;
  }
  else{
    data=data.replace(" ",""); // remove espa�os
  }
  if(data.length != 10) {
    alert("Data Incorreta!");
// document.Form1[campo].focus(); 
// document.Form1[campo].select();
//arrFieldsClearValue[campo] = 1;
    return false;
  }

posbarra1=data.indexOf("/",0);
posbarra2=data.indexOf("/",posbarra1+1);

dia = data.substring(0,posbarra1); 
mes = data.substring(posbarra1+1 , posbarra2); 
ano = data.substring(posbarra2+1,10); 
var observacao = 0;			
var retorno = 1; 
// o sistema  aceita de 1753 a 9999, mas este escript foi limitado desde 1900
if((ano>=1900)&&(ano<=3000)){				
  if(ano > 80 && ano <= 99) // corrige o ano caso foi digitado com apenas dois numeros
    ano = 1900 + parseInt(ano,10);
  else
    ano = 2000 + parseInt(ano,10);					
}else{
  if(data.length>=8){
    observacao = 1; // retorna 2 para mudar exibir mensagem de observacao e nao de erro. 
  }
}
  
if(dia.length==0 || mes.length==0 ){
  retorno = 0; 
}
// verifica o dia valido para cada mes 
if ((dia < 1)||(dia < 1 || dia > 30) && (mes == 4 || mes == 6 || mes == 9 || mes == 11 ) || dia > 31){ 
  retorno =0;
} 
// verifica se o mes e valido 
if (mes < 1 || mes > 12 ){ 
retorno =0;
} 
// verifica se � ano bissexto 
//se o resto divisao do ano por 4 for 0 o ano � bisexto sen�o o ano � normal
if (mes == 2 && ( dia < 1 || dia > 29 || ( dia > 28 && (ano / 4 != ano / 4)))){ 
  retorno =0;
} 
if (valor == ""){ 
  retorno =0;
}             
if (retorno == 0 ) { 
  //alert("Data Incorreta!");
  // document.Form1[campo].focus(); 
  // document.Form1[campo].select();
  //arrFieldsClearValue[campo] = 1;
  return false;
} else if(observacao==1){ // mensagem de observacao
  alert("OBSERVAÇÃO: Este ano não é aceito pelo sistema!");
  // document.Form1[campo].focus(); 
  // document.Form1[campo].select();
  //arrFieldsClearValue[campo] = 1;
  return false;				
}
else{
  //arrFieldsClearValue[campo] = 0;
  return true;
}
    
} 