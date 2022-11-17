Dependencies
  -> CommunityToolkit.Mvvm
  -> Plugin.NFC
  -> Plugin.Fingerprint
  -> Plugin.LocalNotification
  -> banditoth.MAUI.Multilanguage

MauiProgram.cs
  -> DPI -> Services, Configurations
  
AppShell.xaml
  -> Menu
  
AppShell.xaml.cs
  -> Routes

Vereisten:
    -> Home
       => Als je inlogd via vingerafdruk krijg je de calender te zien en als er een error is met de api call krijg je een popup met de error (dit is van toepassing voor alle API calls).
       => De list van de CalendarItems kan je refreshen door te scrollen.

    -> License
       => Hier kan je zoeken naar licenties op naam of licentie nr of met NFC. Dit aan de hand van een API call.
	 => Buiten bij nfc word er gecheckd als je internet hebt. Zoniet worde de kaart uitgelezen maar zonder een afbeelding. Omdat NFC kaarten maar over 500-900bytes heeft kan een afbeelding niet toegevoegd worden. Als er wel internet is word de id van de NFC kaart gebruikt op een api call te doen op id.
       => Als je in portrait mode zit word je door gestuurd naar een nieuwe pagina met de gekoze licentie. En op landscape word het beeld gesplits en krijg je de detail venster aan de rechter kant.
       => De data in de list word per 10 items toegevoegd. Als je verder scrolled haalt hij er nieuwe op. (RecyclerView)
       => De search check of het een getal is zoja zoekt hij op licentie nr ander check het of er een deel overeen komt met de fullname.

    -> ScoreTracker
       => Hier kan je scores bijhouden van meerdere spellen en berekend de gewonnen matchen en de pluspunten voor je. Je kan deze teller ook resetten.

    -> Settings
       => Hier kan je kiezen tussen het light en Darktheme.
       => Taal keuze: Engels en Nederlands. (Alles is vertaald buiten de api en data dat word opgehaald).
       => NFC: Er word gechecked of NFC aanstaat of niet.
       => Deze settings worden in een json file opgeslagen op het device. En worden opnieuw ingeladen bij het opstarten.

Extra's:
 -> FingerPrint Auth.
 -> NFC: Lezen van NFC kaart.
 -> LocalNotification: Push notificatie naar je android.
 -> Connectivity: Controleren of er internet beschikbaar is (je kan hier ook checken op welke hardware: bluethoot, wifi of celluar).
 -> IAlert: Implementatie van Popups in de app.
