import org.openkinect.freenect.*;
import org.openkinect.processing.*;

import processing.io.*;
SoftwareServo servo;
SoftwareServo servo2;
SoftwareServo servo3;
SoftwareServo servo4;

int motorPin = 4;
int motorPin2 = 17;
int motorPin3 = 18;
int motorPin4 = 27;


float xMotor4 = 0;

  Kinect kinect;
  KinectTracker tracker;
  
  PVector v1 = new PVector();
  PVector v2 = new PVector();


void setup(){
  //size(640, 520);
  
  //kinect = new Kinect(this);
  //tracker = new KinectTracker();
  
  
  //servo = new SoftwareServo(this);
  //servo.attach(motorPin);
 
  //servo2 = new SoftwareServo(this);
  //servo2.attach(motorPin2);
 
  //servo3 = new SoftwareServo(this);
  //servo3.attach(motorPin3);
 
  servo4 = new SoftwareServo(this);
  servo4.attach(motorPin4);
 
  //servo.write(90);
  //servo2.write(90);
  //servo3.write(90);
  //servo4.write(90-asin(xMotor4));
}

void draw(){
  
  float diff = 0;
  float angle = 90;
  
  /*tracker.track();
  
  tracker.display();
  

  
  v2 = tracker.getLerpedPos();
  
  if(frameCount == 0){
    v1.x = v2.x;
    
  }
  diff = v2.x-v1.x;
  
  
  
  if(frameCount%180 == 179){
    if(diff<0){
      angle = 20;
    }
    else{
      angle = 160;
    }
    servo4.write(angle);
    
  }*/
  angle = 90+ sin(frameCount/100)*85;

  servo4.write(angle);
  
  //println(angle);
  //print("v1 : " + v1);
  //println("         v2 : " + v2);
  
  v1.x = v2.x;
 
}

void keyPressed(){
  if(key == CODED){
    switch(keyCode){
      case RIGHT : 
        if(xMotor4 > -0.8){
          println("Moving Right to x : " + (xMotor4 - 0.1f));
          xMotor4 -= 0.1 ;
          servo4.write(90-asin(xMotor4));
        }        
        break;
      case LEFT : 
      if(xMotor4 < 0.8){
        xMotor4 += 0.1;
        servo4.write(90-asin(xMotor4));
      }
        break;
      default :
        break;
    }
  }
}
