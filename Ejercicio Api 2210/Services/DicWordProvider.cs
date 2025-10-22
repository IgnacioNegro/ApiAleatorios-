using Ejercicio_Api_2210.Services;
using System.Text;

public class DicWordProvider : IWordProvider
{
    private readonly string[] _words;

    public DicWordProvider(IWebHostEnvironment env)
    {
        var root = env.WebRootPath ?? "wwwroot";
        var dicPath = Path.Combine(root, "dic", "es_ES.dic");
        if (!File.Exists(dicPath))
            throw new FileNotFoundException($"No se encontró el diccionario: {dicPath}");

        var lines = File.ReadAllLines(dicPath, Encoding.UTF8).Skip(1); // salta el conteo

        _words = lines
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => {
                var slash = l.IndexOf('/');
                return slash >= 0 ? l[..slash] : l;  // base antes del '/'
            })
            .Where(w => w.All(ch => char.IsLetter(ch) || "áéíóúñÁÉÍÓÚÑ".Contains(ch)))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (_words.Length == 0)
            throw new InvalidOperationException("No se cargaron palabras desde es_ES.dic.");
    }

    public string GetRandomWord()
    {
        int i = Random.Shared.Next(0, _words.Length);
        return _words[i];
    }
}
