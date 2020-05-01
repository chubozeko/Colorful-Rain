function Drop(x, c, img, speed) {
  this.y = 0;
  this.x = x;
  this.w = 20;
  this.h = 30;
  this.speed = speed;
  this.top = this.y - this.w/2;
  this.bottom = this.y + this.w/2;
  this.colour = c;
  this.img = img;

  this.show = function() {
    // fill(this.colour);
    // rect(this.x, this.y, this.w, this.h);
    tint(this.colour);
    image(this.img, this.x, this.y, this.w, this.h);
  }

  this.update = function() {
    this.y += this.speed;
    this.bottom = this.y + this.w/2;
  }

  this.offscreen = function() {
    if (this.y > height) {
      return true;
    } else {
      return false;
    }
  }

  this.hits = function(bucket) {
    if (this.bottom >= bucket.y) {
      if (this.x > bucket.x-(bucket.w/2) && this.x < bucket.x+(bucket.w/2)) {
        return true;
      }
    }
    return false;
  }
  
  this.compareColours = function(bucket) {
    var areTheSame = [];
    for(let i=0; i<this.colour.length; i++) {
      if(this.colour[i] == bucket.colour[i])
        areTheSame[i] = true;
      else
        areTheSame[i] = false;
    }
    return areTheSame[0] && areTheSame[1] && areTheSame[2];
  }
}