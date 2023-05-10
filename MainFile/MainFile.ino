//green = 3 hp, blue = 2 hp, red = 1 hp
bool threeHP, twoHP, oneHP;

unsigned long myTime;
unsigned long startButtonTime, endButtonTime, buttonPressedTime;

bool countingButtonTime = false;
bool calculateDifferenceButton = false;

void setup() {
  Serial.begin(9600);
  Serial.setTimeout(1);

  pinMode(4, INPUT);

  //green
  pinMode(10, OUTPUT);
  //blue
  pinMode(11, OUTPUT);
  //red
  pinMode(12, OUTPUT);

  threeHP = true;
  twoHP = false;
  oneHP = false;
}

void loop() {
  int buttonState = digitalRead(4);

  if (threeHP) {
    digitalWrite(10, HIGH);
    digitalWrite(11, HIGH);
    digitalWrite(12, HIGH);
  } else if (twoHP) {
    digitalWrite(10, LOW);
    digitalWrite(11, HIGH);
    digitalWrite(12, HIGH);
  } else if (oneHP) {
    digitalWrite(10, LOW);
    digitalWrite(11, LOW);
    digitalWrite(12, HIGH);
  } else if (!threeHP && !twoHP && !oneHP) {
    digitalWrite(10, LOW);
    digitalWrite(11, LOW);
    digitalWrite(12, LOW);
  }

  //press button
  if (!countingButtonTime) {
    if (buttonState == 1) {
      countingButtonTime = true;
      startButtonTime = millis();
    }
  }

  //release button
  if (countingButtonTime) {
    if (buttonState == 0) {
      countingButtonTime = false;
      calculateDifferenceButton = true;
      endButtonTime = millis();
    }
  }

  //calculate difference
  if (calculateDifferenceButton) {
    calculateDifferenceButton = false;

    buttonPressedTime = endButtonTime - startButtonTime;
    Serial.println(buttonPressedTime);
  }

  String unity = Serial.readString();
  int value = unity.toInt();

  //lost one hp
  if (value == 3) {
    threeHP = false;
    twoHP = true;
  } else if (value == 2) {
    //lost 2 hp
    twoHP = false;
    oneHP = true;
  } else if (value == 1) {
    // lost game
    oneHP = false;
    delay(1000);
    threeHP = true;
    twoHP = false;
    oneHP = false;
  }
}
