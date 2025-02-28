using System;

namespace linq_oppgavegjennomgang.Model;

public static class LevenShtein
{
    public static double Lev(string a, string b)
    {
        if (b.Length == 0) return a.Length;
        if (a.Length == 0) return b.Length;
        if (a[0] == b[0]) return Lev(string.Join("",a.Skip(1).ToArray()), string.Join("", b.Skip(1).ToArray()));
        return 1 + Math.Min(
            Lev(string.Join("",a.Skip(1).ToArray()), b),
            Math.Min(Lev(a, string.Join("",b.Skip(1).ToArray())), Lev(string.Join("",a.Skip(1).ToArray()), string.Join("", b.Skip(1).ToArray())))
        );
    }
}
