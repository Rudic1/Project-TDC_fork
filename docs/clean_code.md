## **Clean Code**
- Am wichtigsten ist uns, das man durch einfaches Lesen des Codes erkennen kann was passiert, ohne die Nötigkeit von weiteren Kommentaren. 
- Kommentare nutzen wir lediglich für TO-DOs, um zu kennzeichnen was noch fehlt und bei Code der zu der Zeit des schreibens noch schwer lesbar zu machen ist und später noch refactored werden soll.
- Funktionen werden kurz gehalten und sollen nur EINE logische Aufgabe haben, die aus dem Funktionsnamen hervorgeht.
- In der grundsätzlichen Formatierung sollen Zeilen nicht zu breit werden und logische Absätze gesetzt werden (Block 1 macht xy dann block 2, usw). Aktuell überlegen wir noch ReSharper einzubauen, um die Formatierung einheitlich zu halten.
- Unit-Tests werden dazu kommen sobald das Backend dafür bereit ist. Diese werden dann nach dem Schema "method_condition_task" erstellt. Also z.B. "GetListsForUser_UserExists_ShouldReturnAllListsOfUser"

### **Beispiel**

Am Beispiel der Funktion "FinishList" kann man die Lesbarkeit sehen. Die Aufgabe der Funktion ist eine To-Do Liste abzuschließen. Dafür wird kontrolliert, ob der Benutzer der die Funktion aufruft auch der Ersteller der Liste ist. Denn nur dann wird sie auch abgeschlossen. Die nötige Logik die die Liste im Speicher abschließt ist asugelagert in eine eigene Funktion.

![clean_code_01](https://github.com/user-attachments/assets/590149fd-27dc-4edc-a62d-fc38feaa13c3)

Hier sind noch zwei weitere Beispiele nach dem gleichen Prinzip.

![clean_code_02](https://github.com/user-attachments/assets/afd3297c-ab4a-48ef-be2a-803eaa6fb702)
