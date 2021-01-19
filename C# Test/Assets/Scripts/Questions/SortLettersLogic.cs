using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

//Write a method that modifies an input string, sorting the letters according to a sort order defined by
//a second string. You may assume that every character in the input string also appears somewhere in
//the sort order string. Make your method as fast as possible for long input strings.
public class SortLettersLogic
{
    private List<char> _tempInput = new List<char>();
    private List<char> _characterHits = new List<char>();
    private StringBuilder _builder = new StringBuilder();

    private string SortLetters(string inputAndOutput, string sortOrder)
    {
        var sortOrderArray = sortOrder.ToCharArray();
        _tempInput.Clear();
        _tempInput.AddRange(inputAndOutput.ToCharArray());
        _builder.Clear();
        for (int i = 0; i < sortOrderArray.Length; i++)
        {
            _characterHits.Clear();
            _characterHits = _tempInput.FindAll(x => x == sortOrderArray[i]);
            if (_characterHits.Count == 0)
            {
                continue;
            }
            _tempInput.RemoveAll(x => x == sortOrderArray[i]);
            _builder.Append(_characterHits.ToArray());
        }       

        return _builder.ToString();
    }

    public bool TestSortLetters()
    { 
        string inputAndOutput = "trion world network";
        string order = " oinewkrtdl";
        string expected = "  oooinnewwkrrrttdl";
        bool result = true;
        result &= string.Equals(expected, SortLetters(inputAndOutput, order));

        order = "";
        expected = "";
        result &= string.Equals(expected, SortLetters(inputAndOutput, order));

        order = "ldtrkwenio ";
        expected = "ldttrrrkwwenniooo  ";
        result &= string.Equals(expected, SortLetters(inputAndOutput, order));

        return result;
    }
}
