# Lottron2000 – Legacy .NET Framework Lottery Simulation System (EF6, SQL Server)

Lottron2000 is a **legacy .NET Framework 4.8 lottery simulation system** built using **C#**, **Entity Framework 6**, and **SQL Server**.  
It represents a **real-world, enterprise, monolithic, pre-.NET Core application** originally developed around **2013**.

This repository is preserved for **educational and architectural analysis**, with a focus on **legacy system design, technical debt, refactoring strategy, and modernization planning**.

---

## 📌 Educational Video Content

A series of videos demonstrating how this old legacy code is being rewritten:

**YouTube Playlist:** [Watch the Series](https://youtu.be/P26t5EVz70U)  
**Channel:** [@CodelessDeveloper](https://www.youtube.com/@CodelessDeveloper)

---

## ⚠️ IMPORTANT NOTICE

**This solution DOES NOT compile and DOES NOT include a database.**

It is intentionally incomplete and provided *as-is* as a reference for studying legacy .NET Framework applications.

---

## 🚨 Project Disclaimer (Read Before Cloning)

- ❌ The solution **will not build successfully**
- ❌ The **SQL Server database is not included**
- ❌ Entity Framework connection strings reference a **non-existent local machine** (`ebapela-pc`)
- ❌ This project is **not production-ready**
- ❌ No setup, configuration, or installation support is provided

This repository is **not intended to be executed** without significant reconstruction.

---

## 🎯 Project Purpose & Learning Goals

This project exists to:

- Preserve a **real-world legacy .NET Framework codebase**
- Demonstrate **enterprise-style layered architecture**
- Serve as a reference for **codebase analysis and refactoring**
- Highlight common **pre-.NET Core design patterns**
- Support learning around **technical debt and modernization planning**
- Compare legacy systems with **modern .NET (6 / 7 / 8)** practices

---

## 📌 Project Status

- 🧊 **Frozen legacy codebase**
- 🚫 **No active development**
- 🧪 **Educational / reference only**

A modern rewrite may exist in a separate repository.

---

## 🧰 Technology Stack (.NET Framework 4.8)

- **.NET Framework 4.8**
- **C#**
- **Entity Framework 6 (EF6)**
- **SQL Server**
- **Visual Studio (legacy-era solution)**

---

## 🏗️ Solution Architecture Overview

The solution follows a **traditional multi-project, layered architecture** typical of older **enterprise .NET Framework applications**.

### Solution Structure

```text
Lottron2000/
 ├─ Lottron2000.BusinessLogic
 ├─ Lottron2000.Data
 ├─ Lottron2000.Models
 ├─ Lottron2000.UnitTests
 └─ IntegrationProfilingTests
```

---

## 🧩 Project Breakdown (Multi-Project .NET Solution)

### Lottron2000.BusinessLogic

Core business logic including:

- Lottery number generation
- Simulated draws
- Winning prize calculations
- Playing session workflows

### Lottron2000.Data

Data access layer built with **Entity Framework 6**:

- Database context configuration
- Repository-style data access
- Checksum handling
- SQL Server dependencies (not included)

### Lottron2000.Models

Shared domain models and entity definitions used across the solution.

### Lottron2000.UnitTests

Unit testing project containing:

- Lottery number generation tests
- Simulated draw validation
- Prize calculation tests
- Legacy `App.config` with EF configuration

> ⚠️ Unit tests **cannot run** due to missing database dependencies.

### IntegrationProfilingTests

Contains:

- Integration-style tests
- Performance and profiling experiments

---

## 🗄️ Database & Entity Framework Configuration

- **Database:** SQL Server
- **ORM:** Entity Framework 6
- **Database schema:** ❌ Not included
- **Migrations:** ❌ Not included
- **Connection strings:** Reference a legacy local machine (`ebapela-pc`)

All database-related assets have been **intentionally excluded** from source control.

---

## ❓ Why the Solution Does Not Compile (Missing Database)

The solution does not compile because:

- The required SQL Server database schema is missing
- Entity Framework connection strings reference a non-existent environment
- Certain legacy dependencies are no longer available
- The codebase has not been modernized for current tooling

This behavior is **intentional**.

---

## 📚 Educational & Learning Value

This repository is useful for:

- Studying **legacy .NET Framework enterprise applications**
- Understanding **monolithic, layered, pre-.NET Core architectures**
- Reviewing **Entity Framework 6 patterns**
- Practicing:
  - Codebase analysis
  - Refactoring strategies
  - Technical debt assessment
  - Dependency cleanup
  - Modernization planning
- Comparing legacy systems with **modern .NET best practices**

---

## 🚫 What This Repository Is NOT

- ❌ A runnable application
- ❌ A starter template
- ❌ A tutorial
- ❌ Production-quality software
- ❌ A complete system

---

## 📄 License

This project is provided for **educational and reference purposes**.

**License:** MIT

---

## 📝 Final Notes

If you are reviewing this repository as part of a portfolio:

- Focus on **architecture, structure, and intent**
- Treat this codebase as a **snapshot in time**
- Recognize that best practices have evolved significantly since its original implementation

---

**Use responsibly. Learn intentionally.**
