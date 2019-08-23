#include <SoftwareSerial.h>
#define LEDPIN 11
String readString;
int flag=0,RX_Flag=0;//serial port sign
int i=0;
int j=0;
static long tag=0; 
char Code[14]; 

SoftwareSerial rfid = SoftwareSerial(50,51);

void setup() {  
  Serial.begin(9600);
  rfid.begin(9600);
  //Serial.println("Sistema de contagem de pulseiras");
  pinMode(LEDPIN, OUTPUT);
  digitalWrite(LEDPIN, HIGH);
}
long Read_ID(){   
  char temp;
  static long aux;
  long Num=0;
  while(rfid.available() > 0){
    for(i=0;(rfid.available()>0);i++){
      temp=rfid.read();      
      delay(2);     
      if(temp==0X02){ //recieve the start bit        
        flag=1;
        i=0;
        RX_Flag=0;
      }
      if(flag==1){ //detect the start bit and recieve daad      
        if(temp==0X03){ //detect the end code,        
          flag=0;  //zero clearing
          if(i==13) 
            RX_Flag=1;
          else 
            RX_Flag=0;
          break;
        }
        Code[i]=temp;         
      }       
    }
  }         
  flag=0;
  if(RX_Flag==1){
    for(i=5;i<11;i++){
      Num<<=4;
      if(Code[i]>64)
        Num+=((Code[i])-55);
      else 
        Num+=((Code[i])-48);        
    }
    if(aux == Num)
      return -1;
    aux = Num;            
    Serial.println(Num);      
  }
  //delay(6000);
  return Num;    
}
void loop() {  
  Read_ID();
}
