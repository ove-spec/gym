<?php
// --- INSTÄLLNINGAR ---
$uploadFolder = __DIR__ . "/uploads/";   // Mapp där filer sparas
$maxFileSize = 5 * 1024 * 1024;          // Max 5 MB

// Skapa mappen om den inte finns
if (!is_dir($uploadFolder)) {
    mkdir($uploadFolder, 0775, true);
}

// Kontrollera om fil skickades
if (!isset($_FILES["pdf"])) {
    die("Ingen fil mottagen.");
}

$file = $_FILES["pdf"];

// --- Kontroll 1: Filfel ---
if ($file["error"] !== UPLOAD_ERR_OK) {
    die("Fel vid uppladdning (kod: {$file["error"]}).");
}

// --- Kontroll 2: Maxstorlek ---
if ($file["size"] > $maxFileSize) {
    die("Filen är för stor. Max 5 MB.");
}

// --- Kontroll 3: Kontrollera MIME-typ faktiskt är PDF ---
$finfo = finfo_open(FILEINFO_MIME_TYPE);
$mime = finfo_file($finfo, $file["tmp_name"]);

if ($mime !== "application/pdf") {
    die("Endast PDF-filer är tillåtna.");
}

// --- Kontroll 4: Skapa säkert filnamn ---
$uniqueName = uniqid("pdf_", true) . ".pdf";

// --- Flytta filen till uppladdningsmappen ---
$destination = $uploadFolder . $uniqueName;

if (!move_uploaded_file($file["tmp_name"], $destination)) {
    die("Kunde inte spara filen.");
}

echo "Uppladdning lyckades! Fil sparad som: $uniqueName";
?>
