# Database Schema Documentation

## SQLite Database
Database file located at: `%LOCALAPPDATA%\CollegeIdManagement\Database\college.db`

## Entities
1. **Students**: Stores personal, academic, address, contact, and ID generation status.
2. **Courses**: Stores academic courses (B.A, B.Sc, B.Com, M.A).
3. **Departments**: Stores academic departments.
4. **Sessions**: Academic sessions (e.g., 2026-27).
5. **Users**: User credentials with SHA256 hashed passwords and roles (Admin, Operator, Viewer).
6. **CollegeSettings**: College profile information, address, and ID instructions.
7. **IdCardTemplates**: ID card visual design templates.
8. **AuditLogs**: System activity audit trails.
