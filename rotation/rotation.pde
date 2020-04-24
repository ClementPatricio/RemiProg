import processing.io.*;
SoftwareServo servo;
SoftwareServo servo2;
SoftwareServo servo3;
SoftwareServo servo4;
int motorPin = 4;
int motorPin2 = 17;
int motorPin3 = 18;
int motorPin4 = 27;

void setup(){
 servo = new SoftwareServo(this);
 servo.attach(motorPin);
 
 servo2 = new SoftwareServo(this);
 servo2.attach(motorPin2);
 
 servo3 = new SoftwareServo(this);
 servo3.attach(motorPin3);
 
 servo4 = new SoftwareServo(this);
 servo4.attach(motorPin4);
}

void draw(){
 float angle = 90 + sin(frameCount/100.0) *85;
 servo.write(angle);
 servo2.write(angle);
 servo3.write(angle);
 servo4.write(angle);
 
}
