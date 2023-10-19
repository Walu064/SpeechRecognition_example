from fastapi import FastAPI
from fastapi.responses import JSONResponse
from fastapi import UploadFile
import shutil
import os
import tempfile
import speech_recognition as sr

app = FastAPI()

@app.post("/recognize-speech")
async def recognize_speech(file : UploadFile):
    try:
        # Tymczasowy zapis przesłanego pliku:
        with tempfile.NamedTemporaryFile(delete = False, suffix = ".wav") as temp_wav:
            shutil.copyfileobj(file.file, temp_wav)
            temp_wav_path = temp_wav.name

        # Inicjalizacja obiektu Recognizer
        recognizer = sr.Recognizer()

        # Rozpoznanie mowy z otrzymanego pliku .wav
        with sr.AudioFile(temp_wav_path) as source:
            audio = recognizer.record(source)

        recognized_text = recognizer.recognize_google(audio, language="pl-PL")

        # Usunięcie tymczasowego plik .wav
        os.remove(temp_wav_path)

        return JSONResponse(content={"recognized_text": recognized_text})
    except Exception as e:
        return JSONResponse(content={"error": f"Nie udało się przetworzyć pliku: {str(e)}"}, status_code = 500)
