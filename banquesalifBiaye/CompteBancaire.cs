namespace Banque;

public class CompteBancaire
{
    private readonly string? _nomClient;
    private bool _bloqué = false;

    private CompteBancaire() { }

    public CompteBancaire(string nomClient, double solde)
    {
        _nomClient = nomClient;
        Solde = solde;
    }

    public double Solde { get; private set; }

    public void Débiter(double montant)
    {
        if (_bloqué)
        {
            throw new Exception("Compte Bloqué");
        }

        if (montant > Solde)
        {
            throw new ArgumentOutOfRangeException(nameof(montant), "Montant débité doit être supérieur ou égal au solde disponible");
        }

        if (montant < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(montant), "Le montant retiré doit être Positif");
        }

        Solde -= montant; // soustraire le montant
    }

    public void Créditer(double montant)
    {
        if (_bloqué)
        {
            throw new Exception("Compte Bloqué");
        }

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
        Solde += montant;
    }

    public void Virement(CompteBancaire compteDestination, double montant)
    {
        if (compteDestination == null)
        {
            throw new ArgumentNullException(nameof(compteDestination), "Le compte de destination ne peut pas être null");
        }

        // Débiter du compte source (this)
        Débiter(montant);

        // Créditer au compte destination
        compteDestination.Créditer(montant);
    }

    private void BloquerCompte() { _bloqué = true; }

    private void DéBloquerCompte() { _bloqué = false; }

    public static void Main()
    {
        var cb = new CompteBancaire(nomClient: "Pr Mamadou Samba Camara", solde: 500000);
        cb.Créditer(montant: 100000);
        cb.Débiter(montant: 500000);
        Console.WriteLine($"Solde disponible : {cb.Solde}");
    }
}
