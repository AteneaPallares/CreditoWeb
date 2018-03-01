using System;
using System.ComponentModel.DataAnnotations;

namespace CreditoWeb.Models
{
    public class Tarjeta
    {
        [Required(ErrorMessage = "El número de tarjeta es necesario.")]
        //[CreditCard]
        public string TarjetaNum { get; set; }
        public TipoTarjeta TipoTarjeta { get; set; }

        public bool Valida { get; set; }
     
         public Tarjeta(string tarjetaNum)
        {
            this.TarjetaNum = tarjetaNum;

            Valida = esValida();

            TipoTarjeta = tipoDeTarjeta();            
        }
        bool validando=false;


        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
            int suma=0,dos=0;
            bool cadados=true;
            int total=0;
        
        if(TarjetaNum.Length>12 && TarjetaNum.Length<19){
            validando=true;
            for(int i =TarjetaNum.Length-1;i>=0;i--){
                  dos++;
                if(dos==1){
                    cadados=true;
                }
                if(dos==2){
                    cadados=false;
                    dos=0;
                }
                 if(i>=1 &&cadados==true){
                     int x = (int)Char.GetNumericValue(TarjetaNum[i-1]);
                     int m=(int)Char.GetNumericValue(TarjetaNum[i]);
                     int y=x*2;
                     if(y>9){y=y-9;}
                     suma=suma+y;
                }
                if(cadados==true){
                    int p=(int)Char.GetNumericValue(TarjetaNum[i]);
                    total=total+p;
                }

            }}
            int suma_total=suma+total;
            if(suma_total%10==0 && validando==true){
                TipoTarjeta=TipoTarjeta.NOVALIDA;
                Valida=true;
                
            }
            else{
                Valida=false;
            }
            return Valida;
        }


        /// Si la tarjeta es valida determinar de cuál tipo es VISA, MASTERCARD, AMERICANEXPRESS
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private TipoTarjeta tipoDeTarjeta()
        {
            var soluci=TipoTarjeta.NOVALIDA;
            if(validando==true){
                int m=(int)Char.GetNumericValue(TarjetaNum[0]);
                int r=(int)Char.GetNumericValue(TarjetaNum[1]);
                if(((m==3 && r==4)||(m==3 && r==7))&&TarjetaNum.Length==15){
                    soluci=TipoTarjeta.AMERICANEXPRESS;
                }
                if(((m==5 && r==1)||(m==5 && r==2)||(m==5 && r==3)||(m==5 && r==4)||(m==5 && r==5))&&TarjetaNum.Length==16){
                    soluci= TipoTarjeta.MASTERCARD;
                }
                if((m==4)&&(TarjetaNum.Length==16 || TarjetaNum.Length==13)){
                    soluci=TipoTarjeta.VISA;
                }
            }
            return soluci;
        }



    }

    public enum TipoTarjeta
    {
        VISA,
        MASTERCARD,
        AMERICANEXPRESS,
        NOVALIDA


    }
}