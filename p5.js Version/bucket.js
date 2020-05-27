function Bucket(x, y, colour, img, speed){
    this.x = x;
    this.y = y;
    this.colour = colour;
    this.img = img;
    this.w = 40;
    this.h = 50;
    this.velocity = 0;
    this.speed = speed;
    
    this.show = function(){
        // rectMode(CENTER);
        // fill(this.colour);
        // rect(this.x, this.y, this.w, this.h, 15);  
        imageMode(CENTER);
        tint(this.colour);
        image(this.img, this.x, this.y, this.w, this.h);
    }
    
    this.moveLeft = function(){
        this.velocity -= this.speed;
    }
  
    this.moveRight = function(){
        this.velocity += this.speed;
    }
    
    this.update = function(){
        this.velocity *= 0.9;
        this.x += this.velocity;
    }
  
    this.changeColour = function(c) {
        this.colour = c;
    }
}