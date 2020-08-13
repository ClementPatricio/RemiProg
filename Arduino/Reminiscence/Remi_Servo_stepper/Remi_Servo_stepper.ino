#include <Servo.h>

int pinServo60 = 2;
int pinServo20 = 5;

Servo servo60;
Servo servo20;

bool lightOn = false;
int pos60 = 90;
int pos20 = 90;

String message;
bool messageComplete = false;


// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(1000000);
  // make the pushbutton's pin an input:
  pinMode(LED_BUILTIN, OUTPUT);
  
  servo60.attach(pinServo60);
  servo20.attach(pinServo20);
  
  
  servo60.write(pos60);
  servo20.write(pos20);
}


// the loop routine runs over and over again forever:
void loop() {
  if(messageComplete){
    pos60 = message.substring(1,4).toInt();
    pos20 = message.substring(5,8).toInt();
    messageComplete = false;
    message = "";
  }
  servo60.write(pos60);
  servo20.write(pos20);
  delay(5);        // delay in between reads for stability
}

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    message += inChar;
    // if the incoming character is a newline, set a flag so the main loop can
    // do something about it:
    if (inChar == '\n') {
      messageComplete = true;
    }
  }
}
