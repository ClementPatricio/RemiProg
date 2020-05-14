#include <Servo.h>

int pinServoBase = 2;
int pinServoRight = 3;
int pinServoLeft = 4;

Servo servoBase;
Servo servoLeft;
Servo servoRight;
bool lightOn = false;
int posBase = 90;
int posLeft = 90;
int posRight = 90;
String message;
String base;

// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  // make the pushbutton's pin an input:
  pinMode(LED_BUILTIN, OUTPUT);
  
  servoBase.attach(pinServoBase);
  servoLeft.attach(pinServoLeft);
  servoRight.attach(pinServoRight);
  
  
  servoBase.write(posBase);
  servoLeft.write(posLeft);
  servoRight.write(posRight);
}

// the loop routine runs over and over again forever:
void loop() {
  // print out the state of the button:
  if (Serial.available())
  {
    message = Serial.readString();
    Serial.println(message);
    if (message)
    {
      posBase = message.substring(1,4).toInt();
      posLeft = message.substring(5,8).toInt();
      posRight = message.substring(9).toInt();
    }
  }
  servoBase.write(posBase);
  servoLeft.write(posLeft);
  servoRight.write(posRight);
  delay(16);        // delay in between reads for stability
}
