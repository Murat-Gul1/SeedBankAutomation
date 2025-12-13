# Seed Bank Automation (Tohum Bankası Otomasyonu)

[![Project Demo](https://img.youtube.com/vi/YTbNcolGxd4/0.jpg)](https://www.youtube.com/watch?v=YTbNcolGxd4)

> **Watch the project demonstration video on YouTube:** [Click Here](https://www.youtube.com/watch?v=YTbNcolGxd4)

## Overview
Seed Bank Automation is a comprehensive Windows Desktop application designed to manage, track, and preserve plant seeds. Going beyond simple inventory management, this project integrates modern technologies such as **Artificial Intelligence (AI)**, **IoT Sensor Monitoring**, and a **Blockchain Simulation** to ensure the quality and traceability of the seed catalog.

## Key Features

### 🌱 Smart Inventory & Management
*   Complete cataloging system for various plant and seed types.
*   Detailed tracking of stock levels, prices, and plant properties.
*   Admin panel for advanced product and user management.

### 🤖 AI-Powered Plant Assistant
*   **Powered by Google Gemini (2.5 Flash):** Integrated AI assistant capable of analyzing plant images and answering user questions.
*   **Visual Analysis:** Upload plant photos to identify species or diagnose health issues.
*   **Chatbot:** Intelligent conversational interface for gardening advice.

### 🌡️ IoT Smart Vault (Seed Vault)
*   **Real-time Monitoring:** Simulates a physical seed vault connected via Arduino (`COM4`).
*   **Sensor Data:** Tracks **Temperature**, **Humidity**, **Gas**, **Light**, and **Motion**.
*   **Automated Controls:** Features automatic fan control and alarm systems triggered by critical thresholds (measured via Serial Port).

### 🔗 Blockchain Simulation
*   Implements a custom blockchain structure to simulate secure data immutability.
*   Ensures the integrity and traceability of critical seed data and transaction history.

### 🛒 E-Commerce Module
*   User-friendly shopping cart and order processing system.
*   Sales reporting and order history tracking for users.

## Technology Stack
*   **Platform:** .NET 8.0 (Windows Forms)
*   **Language:** C#
*   **UI Library:** DevExpress
*   **Database:** SQLite (Entity Framework Core)
*   **AI Service:** Google Gemini API
*   **Hardware Interface:** System.IO.Ports (Serial Communication)

## Getting Started
1.  Open `TohumBankasiOtomasyonu.sln` in Visual Studio 2022.
2.  Ensure you have the required **DevExpress** components installed.
3.  Build the solution to restore NuGet packages.
4.  Run the application. (For the Smart Vault feature, an Arduino connected to COM4 is recommended but optional).

---
*Developed by Murat*
