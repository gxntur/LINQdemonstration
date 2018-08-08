// William Guntur wgun828 Compsci 335 Assignment 1
// C# solution using LINQ and lambda expressions. No for, foreach, while statements used aside from Array.ForEach using lambda expression as allowed by the lecturers.
// Tested on lab computers on 30/07/18.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

public class wgun828{
  public static void Main(string[] args){
    try{
      if (args == null || args.Length == 0){
        throw new Exception("Argument cannot be empty. Please enter a filename."); // Throws an error if no arguments are given.
      }
      if (!File.Exists(args[0])){
        throw new Exception("This file doesnt exist in the current directory."); // Throws an error if the filename doesnt exist in the current directory.
      }
      if (args.Length > 1){
        if (Regex.IsMatch(args[1], @"^\d+$") != true){ 
          throw new Exception("Please only enter a valid non-negative number."); // Throws an error if the second argument given is not numeric.
        }
      }
      int lines = 3; // By default, 'k' is set to 3.
      if (args.Length == 2){
        lines = int.Parse(args[1]); // Lines is 'k' as given in the assignment spec.
      }
      var fname = args[0];
      var text = File.ReadAllText(fname);
      Regex rgx = new Regex(@"[^a-zA-Z]+");
      var words = rgx.Replace(text, " ").Trim().Split(); // Replaces all non-alphabet characters with a single space.
      var wordFrequency = Frequencies (words, lines); // Call the function Frequencies, with lines as argument 'k'
      Array.ForEach(wordFrequency.ToArray(), x => Console.WriteLine("{0} {1}", x.Item1, x.Item2) ); // Using an Array.ForEach with lambda expression, allowed by lecturers on Piazza.
    } catch (Exception ex) {
           Console.WriteLine($"*** Error: {ex.Message}"); 
      Environment.ExitCode = 1; 
    } 
    IEnumerable<(int, string)> Frequencies (string[] words, int lines){
      var frs = words
        .GroupBy (s => s.ToUpper()) 
        .Select (g => (g.Count(), g.Key)) 
        .OrderByDescending (kc => kc.Item1)
        .ThenBy(kc => kc.Item2) // Second ordering.
        .Take(lines) // Implementing the second argument 'k'.
        ;
      return frs;
    }
  }
}