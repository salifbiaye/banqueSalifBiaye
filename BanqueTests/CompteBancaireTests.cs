using Banque;

namespace BanqueTests;

[TestClass]
public sealed class CompteBancaireTests
{
    [TestMethod]
    public void VérifierDébitCompteCorrect()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantDébit = 400000;
        const double soldeAttendu = 100000;
        var cb = new CompteBancaire(nomClient: "Pr Ibrahima Fall", soldeInitial);

        // Act
        cb.Débiter(montantDébit);

        // Assert
        Assert.AreEqual(soldeAttendu, cb.Solde, delta: 0.001, message: "Compte débité incorrectement");
    }

    [TestMethod]
    public void DébiterMontantNégatifSoulèveApplicationException()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantDébit = -400000;
        var cb = new CompteBancaire(nomClient: "Pr Ibrahima Mbom", soldeInitial);

        // Act & Assert
        try
        {
            cb.Débiter(montantDébit);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            StringAssert.Contains(ex.Message, "Positif");
        }
    }

    [TestMethod]
    public void DébiterMontantSupérieurSoldeSoulèveException()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantDébit = 600000;
        var cb = new CompteBancaire(nomClient: "Pr Ibrahima Diop", soldeInitial);

        // Act & Assert
        try
        {
            cb.Débiter(montantDébit);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            StringAssert.Contains(ex.Message, "supérieur");
        }
    }

    [TestMethod]
    public void VérifierCréditCompteCorrect()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantCrédit = 100000;
        const double soldeAttendu = 600000;
        var cb = new CompteBancaire(nomClient: "Pr Moussa Diallo", soldeInitial);

        // Act
        cb.Créditer(montantCrédit);

        // Assert
        Assert.AreEqual(soldeAttendu, cb.Solde, delta: 0.001, message: "Compte crédité incorrectement");
    }

    [TestMethod]
    public void CréditerMontantNégatifSoulèveException()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantCrédit = -100000;
        var cb = new CompteBancaire(nomClient: "Pr Aminata Sarr", soldeInitial);

        // Act & Assert
        try
        {
            cb.Créditer(montantCrédit);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Exception attendue
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    public void CréditerMontantZéroSoulèveException()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantCrédit = 0;
        var cb = new CompteBancaire(nomClient: "Pr Fatou Sall", soldeInitial);

        // Act & Assert
        try
        {
            cb.Créditer(montantCrédit);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Exception attendue
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    public void VérifierVirementCorrect()
    {
        // Arrange
        const double soldeInitialSource = 500000;
        const double soldeInitialDestination = 200000;
        const double montantVirement = 100000;
        const double soldeAttenduSource = 400000;
        const double soldeAttenduDestination = 300000;

        var compteSource = new CompteBancaire(nomClient: "Pr Omar Diop", soldeInitialSource);
        var compteDestination = new CompteBancaire(nomClient: "Pr Awa Ndiaye", soldeInitialDestination);

        // Act
        compteSource.Virement(compteDestination, montantVirement);

        // Assert
        Assert.AreEqual(soldeAttenduSource, compteSource.Solde, delta: 0.001, message: "Solde du compte source incorrect après virement");
        Assert.AreEqual(soldeAttenduDestination, compteDestination.Solde, delta: 0.001, message: "Solde du compte destination incorrect après virement");
    }

    [TestMethod]
    public void VirementAvecCompteDestinationNullSoulèveException()
    {
        // Arrange
        const double soldeInitial = 500000;
        const double montantVirement = 100000;
        var compteSource = new CompteBancaire(nomClient: "Pr Cheikh Sy", soldeInitial);

        // Act & Assert
        try
        {
            compteSource.Virement(null!, montantVirement);
            Assert.Fail("Une exception ArgumentNullException aurait dû être levée");
        }
        catch (ArgumentNullException ex)
        {
            StringAssert.Contains(ex.Message, "destination");
        }
    }

    [TestMethod]
    public void VirementAvecMontantSupérieurSoldeSoulèveException()
    {
        // Arrange
        const double soldeInitialSource = 100000;
        const double soldeInitialDestination = 200000;
        const double montantVirement = 150000;

        var compteSource = new CompteBancaire(nomClient: "Pr Aissatou Ba", soldeInitialSource);
        var compteDestination = new CompteBancaire(nomClient: "Pr Ousmane Gueye", soldeInitialDestination);

        // Act & Assert
        try
        {
            compteSource.Virement(compteDestination, montantVirement);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            StringAssert.Contains(ex.Message, "supérieur");
        }
    }

    [TestMethod]
    public void VirementAvecMontantNégatifSoulèveException()
    {
        // Arrange
        const double soldeInitialSource = 500000;
        const double soldeInitialDestination = 200000;
        const double montantVirement = -50000;

        var compteSource = new CompteBancaire(nomClient: "Pr Mariama Kane", soldeInitialSource);
        var compteDestination = new CompteBancaire(nomClient: "Pr Modou Thiam", soldeInitialDestination);

        // Act & Assert
        try
        {
            compteSource.Virement(compteDestination, montantVirement);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            StringAssert.Contains(ex.Message, "Positif");
        }
    }

    [TestMethod]
    public void VirementAvecMontantZéroSoulèveException()
    {
        // Arrange
        const double soldeInitialSource = 500000;
        const double soldeInitialDestination = 200000;
        const double montantVirement = 0;

        var compteSource = new CompteBancaire(nomClient: "Pr Khadija Dieng", soldeInitialSource);
        var compteDestination = new CompteBancaire(nomClient: "Pr Abdou Ndoye", soldeInitialDestination);

        // Act & Assert
        try
        {
            compteSource.Virement(compteDestination, montantVirement);
            Assert.Fail("Une exception ArgumentOutOfRangeException aurait dû être levée");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Exception attendue
            Assert.IsTrue(true);
        }
    }
}
