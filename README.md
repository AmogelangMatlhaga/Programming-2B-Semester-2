# Contract Monthly Claim System (CMCS)

## 📌 Project Overview

The Contract Monthly Claim System (CMCS) is a web-based prototype designed to streamline the submission and management of academic claims. Traditionally, these claims were handled manually or via partially digital processes, resulting in inefficiencies and delays. CMCS solves these issues by providing a secure, transparent, and structured workflow for lecturers, coordinators, and programme management.

## 🎯 Objectives

- Provide a secure and transparent platform for submitting claims.
- Streamline the approval process for lecturers, coordinators, and management.
- Ensure data integrity through robust database design and validation.
- Enhance efficiency, accountability, and transparency in claims management.

## 🚀 Features

- **User Authentication & Authorization:** Role-based access for lecturers, coordinators, and management.
- **Claim Submission:** Lecturers can submit claims with details such as hours worked, module, faculty, and supporting documents.
- **Approval Workflow:** Coordinators review claims before management makes the final decision.
- **Claim Tracking:** Status updates (Pending, Approved, Rejected) with a history of actions.
- **Secure File Uploads:** Document format validation.
- **Automated Calculations:** Claim totals calculated based on sessions, hours, and rates.

## 🏗️ System Architecture

- **Backend:** C#
- **Frontend:** HTML, CSS, JavaScript
- **Database:** MySQL
- **Design Pattern:** Model–View–Controller (MVC)

The MVC architecture ensures scalability, maintainability, and security.

## 📊 UML Class Diagram

- **Users Class:** Attributes include UserID, Full Names, Email, Gender, Role, and Password.
- **Claims Class:** Attributes include ClaimID, Sessions, Hours, Rate, Total Amount, Module, Faculty, Documents, Claim Status, and Date.
- **Relationship:** One-to-many — each lecturer can submit multiple claims.

## 🖥️ GUI Design

- **Login Page:** Secure authentication for all users.
- **Registration Page:** Allows new lecturers to register.
- **Home Page:** Dashboard with role-based navigation.
- **Submit Claim Page:** Form to capture claim details and upload documents.
- **Track Claim Page:** Real-time monitoring of claim status.
- **Coordinator Dashboard:** Review and pre-approve claims.
- **Management Dashboard:** Finalize claims (approval/rejection).

## 📅 Project Plan

- **Weeks 1–2:** Planning & Requirements Gathering
- **Weeks 2–3:** System Design (Database, UML, Wireframes)
- **Weeks 3–6:** Backend Development
- **Weeks 4–7:** Frontend Development
- **Weeks 7–8:** Integration & Testing
- **Week 9:** Deployment & Feedback Review

## 📂 Folder Structure

```
/CMCS
│── /Controllers       # MVC Controllers
│── /Models            # Data Models (Users, Claims)
│── /Views             # Frontend Views (HTML, Razor, etc.)
│── /wwwroot           # Static files (CSS, JS, Images)
│── /Documentation     # Project report, UML diagrams, extra docs
│── appsettings.json   # Configuration file
│── README.md          # Project description
```

## 🔒 Security Considerations

- Secure password storage (hashing)
- Input validation and file upload restrictions
- Role-based access control

## 📖 References

Key references that informed this project:

- Lee, H.-Y. & Wang, N.-J. (2019). Cloud-based enterprise resource planning with elastic model–view–controller architecture for Internet realization.
- Philip, M.M., Seshadri, A. & Vijayakumar, B. (2020). Microservices Centric Architectural Model for Handling Data Stream Oriented Applications.
- Dehbozorgi, N. & Norkham, A. (2021). An Architecture Model of Recommender System for Pedagogical Design Patterns.
- Li, F. & Zhang, J. (2021). Construction and Realization of the Marketing Management Information System for E-commerce Companies.
- Arias-Serna, M.A. et al. (2022). Development of an application for the calculation of credit risk in entities of the Colombian solidarity sector.

## 👤 Author

**Amogelang Refilwe Matlhaga**  
Programming 2B — PROG6212
