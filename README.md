# 🚀 Kiwisquare Casual Jobs

**A Full-Stack Job Aggregation & Automated Poster Generation System**

[![Status](https://img.shields.io/badge/Status-Maintained-brightgreen)]()
[![Tech](https://img.shields.io/badge/Stack-React%20%7C%20.NET%20%7C%20MySQL-blue)]()
[![Location](https://img.shields.io/badge/Market-New%20Zealand-white)]()

---

## 📌 Project Overview

**Kiwisquare Casual Jobs** is an end-to-end solution designed to centralize the fragmented New Zealand casual job market. It independently aggregates job listings from various sources, processes them into a high-quality structured format, and provides an automated tool to generate social-media-ready recruitment posters.

### 🌟 Key Features
*   **Automated Data Pipeline**: Daily scraping of critical job metadata including Title, Location, Contact Details, and Job Descriptions.
*   **Data Integrity & Quality**: Implements automated deduplication and normalization within a MySQL environment to ensure a clean dataset.
*   **Job Dashboard**: A lightweight, responsive React frontend for job listing management and filtering.
*   **Poster Engine**: Automated generation of **1080×1440** high-resolution posters, optimized for Instagram, WeChat, and Little Red Book (Xiaohongshu).
*   **Backend Services**: Robust C#/.NET architecture handling scheduled tasks, data processing, and RESTful API delivery.

---

## 🏗 System Architecture

The system is designed with a focus on data ownership and automated reliability:

1.  **Extraction Layer**: Custom automated processes extract raw data from `Jobi` table.
2.  **Storage Layer (`Jobi`)**: A MySQL database engineered for data quality, performing structuring and normalization on arrival.
3.  **Service Layer**: ASP.NET Core APIs manage the business logic, data scheduling, and secure content delivery.
4.  **Presentation Layer**: A modern React SPA (Single Page Application) for visual data interaction and client-side image rendering.

---

## 🛠 Tech Stack

### Frontend
*   **Library**: React (TypeScript)
*   **Rendering**: `html-to-image` for high-fidelity DOM-to-PNG conversion
*   **API Client**: Axios
*   **Styling**: Modular CSS3 with Flexbox/Grid layouts

### Backend
*   **Framework**: .NET 10 / C#
*   **Architecture**: RESTful API Design
*   **Automation**: Background worker services for daily data synchronization

### Database & DevOps
*   **Database**: MySQL 8.0
*   **Version Control**: Git
*   **Tooling**: Scalar
*   **Project Tracking**: Documentation of system architecture and data pipelines

---

## 📸 Visual Showcase

### 1. Job Management Dashboard


### 2. Generated Social Media Poster (1080×1440)


---

## 🚀 Getting Started

### Prerequisites
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
*   [Node.js 18+](https://nodejs.org/)
*   [MySQL 8.0](https://dev.mysql.com/downloads/installer/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/MikeLiu3030/casual-project
2. **Backend Setup**
    ```bash
    cd casual_backend
    # Update the connection string in appsettings.json
    dotnet restore
    dotnet run
3. **Frontend Setup**
    ```bash
    cd casual_frontend
    npm install
    npm run dev
---
## 🔧 Engineering Implementation Details

*   **Deduplication Strategy**: Jobs are filtered using a multi-factor comparison (URL + Job Metadata) to prevent redundant listings across platforms.
*   **Dynamic Poster Rendering**: Utilizes `html-to-image` with custom canvas settings to capture specific DOM nodes at high pixel density, ensuring readability on mobile displays.
*   **Efficient Filtering**: Implements dynamic query parameters (`?date=YYYY-MM-DD`) allowing the frontend to pull specific daily job batches with minimal latency.

---

## 📬 Contact & Credits

**Developed by Mike Liu**

*   **Primary Focus**: Full-Stack Architecture, Data Engineering, and Market Automation.
*   **Market Coverage**: New Zealand Casual Job Market.
