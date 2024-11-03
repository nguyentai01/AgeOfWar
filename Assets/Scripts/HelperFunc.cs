using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HelperFunc
{
    public static string ConvertValue(double coin)
    {
        string text = coin.toFixed(0); ;
        // if (coin >= 10000000000) text = System.Math.Truncate(coin / 1000000000).ToString() + "B";
        // else if (coin >= 10000000) text = System.Math.Truncate(coin / 1000000).ToString() + "M";
        // else if (coin >= 10000) text = System.Math.Truncate(coin / 1000).ToString() + "K";
        // else text = System.Math.Truncate(coin).ToString();

        if (coin < 1e3)
        {
            text = coin.toFixed(0);
        }
        else if (coin >= 1e3 && coin < 1e6)
        {
            text = Convert((coin / 1e3).toFixed(1)) + "K";
        }
        else if (coin >= 1e6 && coin < 1e9)
        {
            text = Convert((coin / 1e6).toFixed(1)) + "M";
        }
        else if (coin >= 1e9 && coin < 1e12)
        {
            text = Convert((coin / 1e9).toFixed(1)) + "B";
        }
        else if (coin >= 1e12 && coin < 1e15)
        {
            text = Convert((coin / 1e12).toFixed(1)) + "T";
        }
        else if (coin >= 1e15 && coin < 1e18)
        {
            text = Convert((coin / 1e15).toFixed(1)) + "a";
        }
        else if (coin >= 1e18 && coin < 1e21)
        {
            text = Convert((coin / 1e18).toFixed(1)) + "b";
        }
        else if (coin >= 1e21 && coin < 1e24)
        {
            text = Convert((coin / 1e21).toFixed(1)) + "c";
        }
        else if (coin >= 1e24 && coin < 1e27)
        {
            text = Convert((coin / 1e24).toFixed(1)) + "d";
        }
        else if (coin >= 1e27 && coin < 1e30)
        {
            text = Convert((coin / 1e27).toFixed(1)) + "e";
        }
        else if (coin >= 1e30 && coin < 1e33)
        {
            text = Convert((coin / 1e30).toFixed(1)) + "f";
        }
        else if (coin >= 1e33 && coin < 1e36)
        {
            text = Convert((coin / 1e33).toFixed(1)) + "g";
        }
        else if (coin >= 1e36 && coin < 1e39)
        {
            text = Convert((coin / 1e36).toFixed(1)) + "h";
        }
        else if (coin >= 1e39 && coin < 1e42)
        {
            text = Convert((coin / 1e39).toFixed(1)) + "i";
        }
        else if (coin >= 1e42 && coin < 1e45)
        {
            text = Convert((coin / 1e42).toFixed(1)) + "j";
        }
        else if (coin >= 1e45 && coin < 1e48)
        {
            text = Convert((coin / 1e45).toFixed(1)) + "k";
        }
        else if (coin >= 1e48 && coin < 1e51)
        {
            text = Convert((coin / 1e48).toFixed(1)) + "l";
        }
        else if (coin >= 1e51 && coin < 1e54)
        {
            text = Convert((coin / 1e51).toFixed(1)) + "m";
        }
        else if (coin >= 1e54 && coin < 1e57)
        {
            text = Convert((coin / 1e54).toFixed(1)) + "n";
        }
        else if (coin >= 1e57 && coin < 1e60)
        {
            text = Convert((coin / 1e57).toFixed(1)) + "o";
        }
        else if (coin >= 1e60 && coin < 1e63)
        {
            text = Convert((coin / 1e60).toFixed(1)) + "p";
        }
        else if (coin >= 1e63 && coin < 1e66)
        {
            text = Convert((coin / 1e63).toFixed(1)) + "q";
        }
        else if (coin >= 1e66 && coin < 1e69)
        {
            text = Convert((coin / 1e66).toFixed(1)) + "r";
        }
        else if (coin >= 1e69 && coin < 1e72)
        {
            text = Convert((coin / 1e69).toFixed(1)) + "s";
        }
        else if (coin >= 1e72 && coin < 1e75)
        {
            text = Convert((coin / 1e72).toFixed(1)) + "t";
        }
        else if (coin >= 1e75 && coin < 1e78)
        {
            text = Convert((coin / 1e75).toFixed(1)) + "u";
        }
        else if (coin >= 1e78 && coin < 1e81)
        {
            text = Convert((coin / 1e78).toFixed(1)) + "v";
        }
        else if (coin >= 1e81 && coin < 1e84)
        {
            text = Convert((coin / 1e81).toFixed(1)) + "w";
        }
        else if (coin >= 1e84 && coin < 1e87)
        {
            text = Convert((coin / 1e84).toFixed(1)) + "x";
        }
        else if (coin >= 1e87 && coin < 1e90)
        {
            text = Convert((coin / 1e87).toFixed(1)) + "y";
        }
        else
        {
            text = Convert((coin / 1e90).toFixed(1)) + "z";
        }
        // string[] str;
        // if (!text.Contains("."))
        // {
        //     str = text.Split(char.Parse(","));
        // }
        // else
        // {
        //     str = text.Split(char.Parse("."));
        // }
        // if (str.Length > 1)
        // {
        //     if (str[1] == "0" || str[0].Length >= 3)
        //     {
        //         return str[0];
        //     }
        // }
        return text;
    }

    public static string toFixed(this double number, uint decimals)
    {
        return number.ToString("N" + decimals);
    }

    public static string Convert(string value)
    {
        string[] str;
        if (!value.Contains("."))
        {
            str = value.Split(char.Parse(","));
        }
        else
        {
            str = value.Split(char.Parse("."));
        }
        if (str[1] == "0" || str[0].Length >= 3)
        {
            return str[0];
        }
        return value;
    }

    public static double GetPrice(int id)
    {
        return System.Math.Truncate(double.Parse(PlayerPrefs.GetString("Price" + id, "160")));
    }

    public static float Random(double min, double max)
    {
        System.Random rand = new System.Random();
        double range = max - min;
        double sample = rand.NextDouble();
        double scaled = (sample * range) + min;
        return (float)scaled;
    }
}
