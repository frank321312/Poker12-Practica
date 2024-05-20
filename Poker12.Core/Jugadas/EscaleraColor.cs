using Poker12.Core.Jugadas;
namespace Poker12.Core.Jugadas;

public class EscaleraColor : IJugada
{
    public string Nombre => "Escalera de color";
    public byte Prioridad => 2;
    public Resultado Aplicar(List<Carta> cartas)
    {
        if (cartas.Count < 5)
        {
            throw new ArgumentException("No se puede realizar esta jugada");
        }

        bool sonCorazones = cartas.All(x => x.Palo == EPalo.Corazon);
        bool sonDiamantes = cartas.All(x => x.Palo == EPalo.Diamante);
        bool sonPicas = cartas.All(x => x.Palo == EPalo.Picas);
        bool sonTreboles = cartas.All(x => x.Palo == EPalo.Trebol);

        if (sonCorazones || sonDiamantes || sonPicas || sonTreboles)
        {
            List<byte> cartasByte;
            List<byte> valoresByte = Enumerable.Range(1, 13).Select(x => (byte)x).ToList();
            var ordenadasPorValor = cartas.OrderBy(x => x.Valor).ToList();

            cartasByte = ordenadasPorValor.Select(x => (byte)x.Valor).ToList();

            var recortarValoresByte = valoresByte.Where(x => x >= cartasByte.First() && x <= cartasByte.Last()).ToList();

            var valor = recortarValoresByte.Count == 5 ? (byte)ordenadasPorValor.Last().Valor : (byte)0; 

            return new Resultado(Prioridad, valor);
        }

        return new Resultado(Prioridad, (byte)0);
    }
}