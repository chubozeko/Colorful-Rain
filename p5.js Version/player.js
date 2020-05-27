function Player(imgG, c1, c2, imgL, imgR){
    this.y = height-45;
    this.x = width/2;
    this.velocity = 0;
    this.speed = 2;
    this.img = imgG;

    this.bucketSize = 40;
    this.bucketColourL = c1;
    this.bucketColourR = c2;
    this.swapVal = true;

    this.bucketL = new Bucket(this.x-30, this.y-5, c1, imgL, this.speed);
    this.bucketR = new Bucket(this.x+30, this.y-5, c2, imgR, this.speed);

    this.show = function(){
      fill(255);
      imageMode(CENTER);
      image(this.img, this.x, this.y+10, 32, 32);
      stroke(93);
      line(0, this.y+24, this.x-8, this.y+24);
      line(this.x+8, this.y+24, width, this.y+24);
      noStroke();
      this.bucketL.show();
      this.bucketR.show();
    }

    this.moveLeft = function(){
        this.velocity -= this.speed;
        this.bucketL.moveLeft();
        this.bucketR.moveLeft();
    }

    this.moveRight = function(){
        this.velocity += this.speed;
        this.bucketL.moveRight();
        this.bucketR.moveRight();
    }

    this.switchBuckets = function(){
        if(this.swapVal) {
          this.bucketL.changeColour(this.bucketColourR);
          this.bucketR.changeColour(this.bucketColourL);
        } else {
          this.bucketL.changeColour(this.bucketColourL);
          this.bucketR.changeColour(this.bucketColourR);
        }

        this.swapVal = !this.swapVal;
    }

    this.update = function(){
        this.velocity *= 0.9;
        this.x += this.velocity;

        this.bucketL.update();
        this.bucketR.update();

        if(this.bucketR.x > width){
          this.bucketR.x = width;
          this.x = width - 30;
          this.bucketL.x = width - 60;

          this.velocity = 0;
          this.bucketL.velocity = 0;
          this.bucketR.velocity = 0;
        }

        if(this.bucketL.x < 0){
          this.bucketL.x = 0;
          this.x = 0 + 30;
          this.bucketR.x = 0 + 60;

          this.velocity = 0;
          this.bucketL.velocity = 0;
          this.bucketR.velocity = 0;
        }
    }
}
