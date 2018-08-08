// William Guntur wgun828 Compsci 335 Assignment 1
// Node JS solution only using linq-es2015 (external) and fs (internal).
// Tested on lab computers on 30/07/18.

var fs = require('fs')
var Enumerable = require('linq-es2015')

function Read(){
    let fname = process.argv[2]; 
    let text = fs.readFileSync(fname, 'utf8').toUpperCase()
    let k = 3 // By default, the number of lines 'k' is 3.
    if (process.argv[3] !== undefined){
        k = process.argv[3] // If the argument 'k' is provided, 'k' will be changed to what is given.
    }
    let words = text.replace(/[^a-zA-Z ]/g, " ").trim()
    let words2 = words.replace(/\s+/g, ' ').trim().split(' ')
    let frequency = Frequencies(words2, k)
    frequency.forEach(s => console.log(s[0].toString(), s[1]))
} 

function Frequencies(words, lines){
    let frs = Enumerable.asEnumerable(words)
        .GroupBy (s => s.toUpperCase()) 
        .Select (g => [g.length, g.key]) 
        .OrderByDescending (kc => kc[0])
        .ThenBy(kc => kc[1])
        .Take(lines) // Limit the number of lines output to 'k'.
        .ToArray ()
      return frs
} 

function Main(){
    try{
        if (process.argv[2] === undefined) {
            throw new Error("Please enter a filename.") // If no filename is given.
        } else if (fs.existsSync(process.argv[2]) == false){
            throw new Error("File does not exist.") // If a filename is specified but does not exist in current directory.
        } if (process.argv[3] !== undefined){
            if (isNaN(process.argv[3])){
                throw new Error("Please enter a valid number for the number of lines to print.")  // If a non-numeric or negative input is given for 'k'.
            } else if (process.argv[3] < 0) {
                throw new Error("Please enter a non-negative number.")
            }
        }
        Read()
    } catch(ex) {
        console.log('*** Error: ' + ex.message)
    }
}
Main()