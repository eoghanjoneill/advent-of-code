const readline = require('readline');
const fs = require('fs');
let map = []
let map = buildMap('C:\\dev\\advent\\csi-north-pole\\example.txt');


function buildMap(filePath) {
  const readInterface = readline.createInterface({
    input: fs.createReadStream(filePath),
    output: process.stdout,
    console: false
  });


  readInterface.on('line', function(line) {
    console.log(line);
  });

}

function processLine(line) {
  line.trim().split(',');

}


