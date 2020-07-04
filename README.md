# DictApp
Final project of NT106.K21.ANTN class

# Prerequisite
SQL Server
- Run CREATE.sql in Database to create new Database on your machine. This registry consists of Users data & History translate request of users.

# Manual
## CODE fields description
Meaning of number of CODE field in Data class.
| CODE  | Description |
| ----- | -----       |
| 100   | Authentication request (login) |
| 102   | Authentication succeed |
| 104   | Authentication request was failed  |
| 200 | Registration request |
| 202 | Registration succeed |
| 204 | Registration failed |
| 300 | Translation request |
| 302 | Translation succeed |
| 304 | Translation not found |
| 400 | New connection request |
| 402 | New connection established successfully |
| 404 | New connection was failed to established |
CODE values 40X are for expansion feature Load Balancing
User's translation history is stored locally at User's machine in an Excel file.
