var circles = [];

function Circles() {
  this.w = 200;
  this.h = 200;
  
  this.createArt = function(c1) {
    let img = createImage(this.w, this.h);
    img.background(245);
    
    var overlapping = false;
    var protection = 0;
    while (circles.length < 300) {
      let c0 = random(c1);
        var circle = {
            x: random(width),
            y: random(height),
            r: random(5, 36),
            red: red(c0),
            green: green(c0),
            blue: blue(c0)
        };
        
        var overlapping = false;
        for (var j = 0; j < circles.length; j++){
            var other = circles[j];
            var d = dist(circle.x, circle.y, other.x, other.y);
            if (d < circle.r + other.r){
                overlapping = true;
            }
        }
        
        if(!overlapping){
            circles.push(circle);
        } 
        
        protection++;
        
        if (protection > 10000){
            break;
        } 
    }
    
    for (var i = 0; i < circles.length; i++){   // Colourful
        fill(circles[i].red, circles[i].green, circles[i].blue, random(100, 255));
        noStroke();
        ellipse(circles[i].x, circles[i].y, circles[i].r*2, circles[i].r*2);
    }
    
    
  }
}