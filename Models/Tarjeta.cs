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
        


        /// Basado en el algoritmo de Luhn determinar si esta tarjeta es valida
        /// como estamos dentro de la clase de tarjeta tenemos acceso a la propiedad TarjetaNum 
        private bool esValida()
        {
            int suma=0,dos=0;
            bool cadados=true;
            int total=0;
            bool validando=false;
        
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
            if(Valida==true){
                int m=(int)Char.GetNumericValue(TarjetaNum[0]);
                int r=(int)Char.GetNumericValue(TarjetaNum[1]);
                int o=(int)Char.GetNumericValue(TarjetaNum[2]);
                int p=(int)Char.GetNumericValue(TarjetaNum[3]);
                int t=(int)Char.GetNumericValue(TarjetaNum[4]);
                int u=(int)Char.GetNumericValue(TarjetaNum[5]);

                if(((m==3 && r==4)||(m==3 && r==7))&&TarjetaNum.Length==15){
                    soluci=TipoTarjeta.AMERICANEXPRESS;
                }
                if(((m==5 && r==1)||(m==5 && r==2)||(m==5 && r==3)||(m==5 && r==4)||(m==5 && r==5))&&TarjetaNum.Length==16){
                    soluci= TipoTarjeta.MASTERCARD;
                }
                if((m==4)&&(TarjetaNum.Length==16 || TarjetaNum.Length==13)){
                    soluci=TipoTarjeta.VISA;
                }
                //Dinersclub
                //300-305, 309, 36, 38-39
                if((m==3 && r==0 && o==0)||(m==3 && r==0 && o==1)||(m==3 && r==0 && o==2)||(m==3 && r==0 && o==3)||(m==3 && r==0 && o==4)||(m==3 && r==0 && o==5)||(m==3 && r==0 && o==9)||(m==3 && r==6)||(m==3 && r==8)||(m==3 && r==9)||(m==3 && r==0 && o==7)){
                     soluci=TipoTarjeta.DINERSCLUB;
                }
                 //Jcb
                //3528-3589
                for(int i=28;i<=89;i++){
                    string ok=i.ToString();
                    int y=(int)Char.GetNumericValue(ok[0]);
                    int q=(int)Char.GetNumericValue(ok[1]);
                if((m==3 && r==5 && o==y && p==q)){
                     soluci=TipoTarjeta.JCB;
                     break;
                }
                }
                //Discover
                //​6011, 622126-622925, 644-649, 65
                 for(int i=126;i<=925;i++){
                    string ok=i.ToString();
                    int y=(int)Char.GetNumericValue(ok[0]);
                    int q=(int)Char.GetNumericValue(ok[1]);
                    int z=(int)Char.GetNumericValue(ok[2]);
                if((m==6 && r==2 && o==2 && p==y && t==q && u==z)||(m==6 && r==0 && o==1 && p==1)||(m==6 && r==4 && o==4)||(m==6 && r==4 && o==5)||(m==6 && r==4 && o==6)||(m==6 && r==4 && o==7)||(m==6 && r==4 && o==8)||(m==6 && r==4 && o==9)||(m==6 && r==5)){
                     soluci=TipoTarjeta.DISCOVER;
                     break;
                }
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
        DINERSCLUB,
        JCB,
        DISCOVER,
        NOVALIDA


    }
}