# AVGameRPG 

**AVGameRPG** to lekka gra RPG stworzona w Avalonia (C#/.NET).  
Gracz rozwija swoją postać, podejmuje misje, zdobywa doświadczenie i ekwipunek.  
Celem rozgrywki jest **pokonanie głównego bossa** oraz osiągnięcie **20 poziomu**.

---

## Funkcje gry

- **Postać gracza**
  - Gracz posiada punkty życia (HP) i many (MP)
  - Gracz dysponuje statystykami: Atak, Obrona, Zręczność, Inteligencja
  - Gracz zdobywa poziomy i doświadczenie
  - Gracz gromadzi złoto, które może wydawać u handlarzy

- **Rozwój**
  - Gracz otrzymuje doświadczenie za walki i misje
  - Gracz automatycznie awansuje na kolejne poziomy
  - Ostatecznym celem jest osiągnięcie **20 poziomu**

- **Misje i questy**
  - Gracz wykonuje misje fabularne i poboczne
  - **Misje bitewne oznaczone są jako `(battle)`** – to starcia z przeciwnikami
  - Czas trwania misji został **skrócony**, aby szybciej je kończyć
  - Nagrody za misje zostały **zwiększone**, co przyspiesza progres
  - **Ostatnia misja – pokonanie głównego bossa – odblokowuje się dopiero po osiągnięciu 20 poziomu**

- **Ekwipunek i przedmioty**
  - Gracz zdobywa różne przedmioty: broń, zbroje, pierścienie, amulety, tarcze, buty itd.
  - Założone przedmioty modyfikują statystyki gracza
  - Gracz może zdejmować i zakładać ekwipunek w dowolnym momencie

- **System zapisu i wczytywania**
  - Gra tworzy pliki zapisu w katalogu `saves/`
  - Gracz może wczytać dowolny slot z listy
  - Zapis zawiera:
    - stan postaci gracza (HP, Mana, Level, EXP, złoto, statystyki)
    - ekwipunek i inventory
    - listę ukończonych questów

---

## Cel gry

Gra **nie ma twardo zdefiniowanego zakończenia w kodzie**.  
**Niepisany „koniec gry”** następuje, gdy:

- gracz **pokona głównego bossa**, oraz  
- gracz osiągnie **20 poziom**.  

---

## Balans i ułatwienia

> W tej wersji gry wprowadzono zmiany, aby gracz mógł szybciej ukończyć rozgrywkę:

- **nagrody** (złoto, przedmioty, doświadczenie) są **zwiększone**,  
- **czas trwania misji** został **skrócony**.  

---

## Obsługa gry

- **Menu główne**
  - **Play** — gracz rozpoczyna nową grę
  - **Load** — gracz wybiera zapis i kontynuuje rozgrywkę
  - **Exit** — gracz zamyka grę

- **Podczas gry**
  - gracz podejmuje misje i walczy z przeciwnikami
  - gracz rozwija statystyki i zdobywa złoto
  - gracz zakłada i zdejmuje przedmioty, aby modyfikować parametry
  - gracz może zapisać stan gry (tworzony jest nowy plik `save_YYYY-MM-DD_HH-mm-ss.json`)

---

## Zapis gry

Każdy zapis znajduje się w katalogu `saves/` w formacie JSON, np.:

