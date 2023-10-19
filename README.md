# SpeechRecognition_example

## 1. Instalacja i uruchomienie API

Aby zainstalować i uruchomić API, wykonaj poniższe kroki:

1. Sklonuj repozytorium z kodem źródłowym API na swój lokalny komputer:
```shell
git clone https://github.com/Walu064/SpeechRecognition_example
```
2. Przejdź do katalogu projektu:
```shell
cd scieżka_do_katalogu_zawierającego_sklonowane_repozytorium/SpeechRecognition_example_backend
```

3. Zainstaluj zależności przy użyciu menedżera pakietów pip:
```shell
pip install -r requirements.txt
```

4. Uruchom API:
```shell
python main.py
```

API powinno być teraz dostępne pod adresem http://localhost:8080/.

![Tu powinien być obrazek, przepraszam za utrudnienia](/images/img1readme.png)

## 2. Kompilacja i uruchomienie aplikacji desktopowej
Aby skompilować i uruchomić aplikację desktopową, wykonaj następujące czynności:

1. Otwórz rozwiązanie ".sln" w środowisku Visual Studio.

2. Skompiluj rozwiązanie.
3. Jeżeli API jest uruchomione, można działać.

## 3. Instrukcja obsługi
![Tu powinien być obrazek, przepraszam za utrudnienia.](/images/img2readme.png)

Przycisk "Rozpocznij/zakończ nagrywanie" rozpoczyna nagrywanie mowy. Po skończeniu zdania, należy go wcisnąć ponownie w celu zakończenia nagrywania.

Przycisk "Wyświetl rozpoznany tekst" wysyła żądanie do API zawierające nagranie w formacie .wav, następnie wyświetla rozpoznany tekst w polu tekstowym poniżej.

Przycisk "Wyczyść pole tekstowe" przywraca wspominane pole tekstowe do stanu, w którym jest ono po uruchomieniu aplikacji.