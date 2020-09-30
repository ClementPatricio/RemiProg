#include <Servo.h>

int pinServo60 = 9;
int pinServoInv60 = 5;
int pinServo20 = 6;

Servo servo60;
Servo servoInv60;
Servo servo20;

bool lightOn = false;
int pos60 = 90;
int posInv60 = 90;
int pos20 = 90;

String message;
bool messageComplete = false;

float stepperAngle = 0;
float stepperOffset = 0;
int stepperOffsetDir = 0;
bool inverse= false;
int delayStep = 3;
float targetStepAngle = 90;
int lastDir = 1;
bool stepHasMove = false;


// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(1000000);
  // make the pushbutton's pin an input:
  pinMode(LED_BUILTIN, OUTPUT);
  
  servo60.attach(pinServo60);
  servoInv60.attach(pinServoInv60);
  servo20.attach(pinServo20);
  servo60.write(pos60);
  servoInv60.write(posInv60);
  servo20.write(pos20);


  //stepper
  pinMode(3, OUTPUT); // Déclaration de la broche n°3 en sortie Digitale PWM
  pinMode(11, OUTPUT); // Déclaration de la broche n°11 en sortie Digitale PWM
  pinMode(12, OUTPUT); // Déclaration de la broche n°12 en sortie Digitale
  pinMode(13, OUTPUT); // Déclaration de la broche n°13 en sortie Digitale
  
}


// the loop routine runs over and over again forever:
void loop() {
  if(messageComplete){
    pos60 = message.substring(1,4).toInt();
    pos20 = message.substring(5,8).toInt();
    targetStepAngle = message.substring(9,12).toInt();
    stepperOffsetDir = message.substring(12,13).toInt() - 1;
    messageComplete = false;
    message = "";
    
  }
  if(pos60 < 30) pos60 = 30;

  posInv60 = abs(pos60 -180);
  servo60.write(pos60);
  servo20.write(pos20);
  servoInv60.write(posInv60);
  //delay(5);        // delay in between reads for stability
 
  UpdateStepper();
}


void UpdateStepper(){
  stepHasMove = false;
  int dir = GetDirection(stepperAngle,targetStepAngle);
  
  if(targetStepAngle < stepperAngle - 3.60){
    if(lastDir != -1) delay(500);
     MoveOneStep(-1);
     stepperAngle -= 1.80;
     lastDir = -1;
     stepHasMove = true;
  }

  if(targetStepAngle > stepperAngle + 3.60){
     if(lastDir != 1) delay(500);
    MoveOneStep(1);
    stepperAngle += 1.80;
    lastDir = 1;
    stepHasMove = true;
  }

  stepperAngle = ConvertAngle(stepperAngle);

  if(stepperOffsetDir < 0){
    MoveOneStep(-1);
    stepperOffset -= 1.80;
  }


   if(stepperOffsetDir > 0){
    MoveOneStep(1);
    stepperOffset += 1.80;
  }

  stepperOffset = ConvertAngle(stepperOffset);
 
  if(!stepHasMove){
      lastDir = 0;
  }
}

//return 0 , 1 or -1 
int GetDirection(float currentAngle, float targetAngle){
   float c = ConvertAngle(currentAngle);
   float t = ConvertAngle(targetAngle);

  float delta = t - c;
  if(abs(delta) < 3.6){
    return 0; //no need to move
  }

  int dir = sign(delta);
  if(abs(delta) > 180){
    dir *= -1;
  } 
}

float ConvertAngle(float a){
   if(a > 360){
    a -= 360;
  }

  if(a < 0){
    a += 360;
  }
    return a;
}

int sign(float value) {
 return (value>0)-(value<0);
}


void MoveOneStep(int direction){
  
 
  if(direction  > 0){
      // Pas n°4 | Sortie A+ du Shield Moteur -> Bobine D du moteur pas à pas
    digitalWrite(12, HIGH);
    digitalWrite(13, LOW);   
    analogWrite(3, 255);
    analogWrite(11, 0);
    delay(delayStep); 
  
     // Pas n°3 | Sortie B+ du Shield Moteur -> Bobine B du moteur pas à pas
    digitalWrite(12, LOW);
    digitalWrite(13, HIGH);  
    analogWrite(3, 0);
    analogWrite(11, 255);
    delay(delayStep); 
  
     // Pas n°2 | Sortie A- du Shield Moteur -> Bobine C du moteur pas à pas
    digitalWrite(12, LOW);
    digitalWrite(13, HIGH);   
    analogWrite(3, 255);
    analogWrite(11, 0);
    delay(delayStep); 
  
    // Commande moteur pas à pas Bipolaire 4 fils en Mode Wave | Sens Normal
    // Pas n°1 | Sortie B- du Shield Moteur -> Bobine A du moteur pas à pas
    digitalWrite(12, HIGH);
    digitalWrite(13, LOW);  
    analogWrite(3, 0);
    analogWrite(11, 255);
    delay(delayStep);
    
  }else{
    // Commande moteur pas à pas Bipolaire 4 fils en Mode Wave | Sens Normal
    // Pas n°1 | Sortie B- du Shield Moteur -> Bobine A du moteur pas à pas
    digitalWrite(12, HIGH);
    digitalWrite(13, LOW);  
    analogWrite(3, 0);
    analogWrite(11, 255);
    delay(delayStep);
     // Pas n°2 | Sortie A- du Shield Moteur -> Bobine C du moteur pas à pas
    digitalWrite(12, LOW);
    digitalWrite(13, HIGH);   
    analogWrite(3, 255);
    analogWrite(11, 0);
    delay(delayStep); 
     // Pas n°3 | Sortie B+ du Shield Moteur -> Bobine B du moteur pas à pas
    digitalWrite(12, LOW);
    digitalWrite(13, HIGH);  
    analogWrite(3, 0);
    analogWrite(11, 255);
    delay(delayStep); 
     // Pas n°4 | Sortie A+ du Shield Moteur -> Bobine D du moteur pas à pas
    digitalWrite(12, HIGH);
    digitalWrite(13, LOW);   
    analogWrite(3, 255);
    analogWrite(11, 0);
    delay(delayStep);
  }


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
