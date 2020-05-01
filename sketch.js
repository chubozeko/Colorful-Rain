var viewer = [true, false, false, false, false, false, false];
let canvas;
var player;
var drops = [];
var levelNr;
let dropSpeed = 2, levelIncr = 40, levelTotal = 8;
var colours = [[], []];
var scores = [0, 0];
let img_Drop, img_BL, img_BR, img_bg1, img_bg2, img_bg3, img_bg4, img_bg6, img_bg7;
var btnPlay, btnNext, btnBack, btnStartGame, btnReplay, btnLvlSel, btnColSel, btnMenu;
var btnLevels = [];

var Bucket_1 = '#b441ff';
var Bucket_2 = '#b4a519';
var gui1;
var b1, b2;

function preload() {
  img_Drop = loadImage('assets/paint_drop.png');
  img_BL = loadImage('assets/bucket_L.png');
  img_BR = loadImage('assets/bucket_R.png');
  img_bg1 = loadImage('assets/1.png');
  img_bg2 = loadImage('assets/2.png');
  img_bg3 = loadImage('assets/3.png');
  img_bg4 = loadImage('assets/4.png');
  img_bg6 = loadImage('assets/6.png');
  img_bg7 = loadImage('assets/7.png');
}

function setup() {
  createCanvas(640, 480);
  
  setupMainMenu();
  setupHowToPlay();
  setupLevelSelection();
  //setupColorSelection();
  //setupGame();
  setupLevelComplete();
  //setupPaused();
    //frameRate(30);
    //player = new Player([180, 65, 255], [180, 165, 25], img_BL, img_BR);
} 

function draw() {
  
  if(viewer[0]) {
    showMainMenu();
  } else if(viewer[1]) {
    showHowToPlay();
  } else if(viewer[2]) {
    showLevelSelection();
  } else if(viewer[3]) {
    showColorSelection();
  } else if(viewer[4]) {
    if(!checkGameOver())
      showGame();
    else 
      viewer = [false, false, false, false, false, true, false];
  } else if(viewer[5]) {
    showLevelComplete();
  } else if(viewer[6]) {
    showPaused();
  }
  
}

function setupGame() {
  player = new Player(colours[0], colours[1], img_BL, img_BR);
  
  loadLevel(levelNr);
}
  
function showGame() {
    background(245, 245, 245);
  
    checkKeyPressed();
  
    // if(!checkGameOver()) {
  
      player.update();
      player.show();

      if(frameCount % 50 == 0){
          var index = random(colours);
          drops.push(new Drop(random(0, width), index, img_Drop, dropSpeed));
      }

      for (var i = drops.length-1; i >= 0; i--){
          drops[i].show();
          drops[i].update();

          if (drops[i].hits(player.bucketL)){
            if (drops[i].compareColours(player.bucketL))
              scores[0]++;
            else
              scores[1]--;

            drops.splice(i, 1);
            console.log('B1: ' + scores[0] + 'B2: ' + scores[1]);
          }

          if (drops[i].hits(player.bucketR)){
            if (drops[i].compareColours(player.bucketR))
              scores[1]++;
            else
              scores[0]--;

            drops.splice(i, 1);
            console.log('B1: ' + scores[0] + 'B2: ' + scores[1]);
          }
        
        

          if(drops[i].offscreen()){
              drops.splice(i, 1);
          }
      }   

      fill(colours[0]);
      rect(0, 0, scores[0] * levelIncr, 20);
      fill(colours[1]);
      rect(width, 0, -scores[1] * levelIncr, 20);
      stroke(255);
      line(width/2, 0, width/2, 20);
      noStroke();
    
    // } else {
    //   viewer = [false, false, false, false, false, true, false];
    //   return;
    // }
}

function checkKeyPressed(){
    if (keyIsDown(65)) { // A
    player.moveLeft();
  }

  if (keyIsDown(68)) { // D
    player.moveRight();
  }

  // if (keyIsDown(UP_ARROW)) {
  //   
  // }
}

function keyPressed(){
    if (keyCode == 83){
        player.switchBuckets();
        reverse(colours);
        reverse(scores);
    }
  return false;
}

function checkGameOver() {
  if(scores[0] == scores[1] && scores[0] == levelTotal) {
    viewer = [false, false, false, false, false, true, false];
    return true;
  } else {
    return false;
  }
}

function loadLevel(level) {
  switch(level) {
    case 1: {
      dropSpeed = 2; 
      levelIncr = 40; 
      levelTotal = 8;
      break;
    }
    case 2: {
      dropSpeed = 2.5; 
      levelIncr = 32; 
      levelTotal = 10;
      break;
    }
    case 3: {
      dropSpeed = 3.5; 
      levelIncr = 20; 
      levelTotal = 16;
      break;
    }
    case 4: {
      dropSpeed = 4;
      levelIncr = 20; 
      levelTotal = 16;
      break;
    }
    case 5: {
      dropSpeed = 4;
      levelIncr = 32; 
      levelTotal = 10;
      break;
    }
    case 6: {
      dropSpeed = 4.5;
      levelIncr = 32;
      levelTotal = 10;
      break;
    }
    case 7: {
      dropSpeed = 5; 
      levelIncr = 40; 
      levelTotal = 8;
      break;
    }
    case 8: {
      dropSpeed = 5.5;
      levelIncr = 40;
      levelTotal = 8;
      break;
    }
    case 9: {
      dropSpeed = 6;
      levelIncr = 64; 
      levelTotal = 5;
      break;
    }
    case 10: {
      dropSpeed = 6.5;
      levelIncr = 64;
      levelTotal = 5;
      break;
    }
    default: {
      dropSpeed = 3; 
      levelIncr = 40; 
      levelTotal = 8;
      break;
    }
  }
}

function setupMainMenu() {
  btnPlay = new Clickable();
  btnPlay.resize(120, 60);
  btnPlay.textSize = 24;
  btnPlay.locate(width/2 - 60, 320);
  btnPlay.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "PLAY";
	this.textColor = "#000000";
  }
  btnPlay.onPress = function(){
    viewer = [false, true, false, false, false, false, false];
  }
}

function showMainMenu() {
  background(245, 245, 245);
  
  image(img_bg1, 0, 0, 640, 480);
  
  btnPlay.draw();
}

function setupHowToPlay() {
  btnNext = new Clickable();
  btnNext.resize(120, 60);
  btnNext.textSize = 24;
  btnNext.locate(width/2 - 60, 400);
  btnNext.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "NEXT";
	this.textColor = "#000000";
  }
  btnNext.onPress = function(){
    viewer = [false, false, true, false, false, false, false];
  }
}

function showHowToPlay() {
  background(245, 245, 245);
  
  image(img_bg2, 0, 0, 640, 480);
  
  btnNext.draw();
}

function setupLevelSelection() {
  for(let i=0; i<10; i++) {
    btnLevels.push(new Clickable());
  }
  
  for(let i=0; i<btnLevels.length; i++) {
    //btnLevels[i] = new Clickable();
    btnLevels[i].resize(50, 50);
    btnLevels[i].textSize = 16;
    btnLevels[i].locate(10 + i*64, 320);
    btnLevels[i].onOutside = function(){
      this.color = "#EEEEEE";
      this.text = "Level " + (i+1);
      this.textColor = "#000000";
    }
    btnLevels[i].onPress = function(){
      viewer = [false, false, false, true, false, false, false];
      levelNr = i+1;
      setupColorSelection();
    }
  }
}

function showLevelSelection() {
  background(245, 245, 245);
  
  image(img_bg3, 0, 0, 640, 480);
  
  for(let i=0; i<btnLevels.length; i++) {
    btnLevels[i].draw();
  }
}

function setupColorSelection() {
  gui1 = createGui('Choose colours:').setPosition(220, 200);
  colorMode(RGB);
  gui1.addGlobals('Bucket_1', 'Bucket_2');
  
  btnStartGame = new Clickable();
  btnStartGame.resize(120, 60);
  btnStartGame.textSize = 24;
  btnStartGame.locate(width/2 - 60, 400);
  btnStartGame.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "START GAME";
	this.textColor = "#000000";
  }
  btnStartGame.onPress = function(){
    
    let strCopy = Bucket_1.split('#');
    let c1 = unhex(strCopy[1].match(/.{1,2}/g));
    let strCopy1 = Bucket_2.split('#');
    let c2 = unhex(strCopy1[1].match(/.{1,2}/g));
    colours[0] = c1.map(Number);
    colours[1] = c2.map(Number);
    console.log('C1: ' + colours[0] + ' C2: ' + colours[1]);
    
    gui1.hide();
    setupGame();
    viewer = [false, false, false, false, true, false, false];
  }
}

function showColorSelection() {
  background(245, 245, 245);
  
  image(img_bg4, 0, 0, 640, 480);
  
  if(viewer[3]) {
    btnStartGame.draw();
    gui1.show();
  }
}

function setupLevelComplete() {
  // btnReplay, btnLvlSel, btnColSel, btnMenu
  btnReplay = new Clickable();
  btnReplay.resize(120, 60);
  btnReplay.textSize = 24;
  btnReplay.locate(120 + 0*64, 320);
  btnReplay.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "RESTART";
	this.textColor = "#000000";
  }
  btnReplay.onPress = function(){
    viewer = [false, false, false, false, true, false, false];
  }
  
  btnLvlSel = new Clickable();
  btnLvlSel.resize(120, 60);
  btnLvlSel.textSize = 24;
  btnLvlSel.locate(120 + 1*64, 320);
  btnLvlSel.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "SELECT LEVEL";
	this.textColor = "#000000";
  }
  btnLvlSel.onPress = function(){
    viewer = [false, false, true, false, false, false, false];
  }
  
  btnColSel = new Clickable();
  btnColSel.resize(120, 60);
  btnColSel.textSize = 24;
  btnColSel.locate(120 + 2*64, 320);
  btnColSel.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "SELECT COLOR";
	this.textColor = "#000000";
  }
  btnColSel.onPress = function(){
    viewer = [false, false, false, true, false, false, false];
  }
  
  btnMenu = new Clickable();
  btnMenu.resize(120, 60);
  btnMenu.textSize = 24;
  btnMenu.locate(120 + 3*64, 320);
  btnMenu.onOutside = function(){
	this.color = "#EEEEEE";
	this.text = "MAIN MENU";
	this.textColor = "#000000";
  }
  btnMenu.onPress = function(){
    viewer = [true, false, false, false, false, false, false];
  }
}

function showLevelComplete() {
  background(245, 245, 245);
  
  image(img_bg1, 0, 0, 640, 480);
  
  // btnReplay, btnLvlSel, btnColSel, btnMenu
  btnReplay.draw();
  btnLvlSel.draw();
  btnColSel.draw();
  btnMenu.draw();
}