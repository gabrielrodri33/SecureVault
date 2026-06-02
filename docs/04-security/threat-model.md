# Threat Model

> This section will be populated during the security analysis phase, after the pentest. The headings below define the structure; fill each section as findings emerge.

---

## Assets

| Asset | Description | Sensitivity |
|-------|-------------|-------------|
| Vault item passwords | Encrypted credentials stored in the database | Critical |
| User account credentials | Email + BCrypt password hash | High |
| JWT access tokens | Short-lived bearer tokens (15 min) | High |
| Refresh tokens | Long-lived tokens stored in DB (7 days) | High |
| User PII | Email, name | Medium |
| Admin access | Elevated privileges over all users and data | Critical |

---

## Threat Actors

| Actor | Description | Motivation |
|-------|-------------|-----------|
| Unauthenticated attacker | No account on the platform | Data theft, account takeover |
| Authenticated regular user | Has a valid account | Access other users' data, privilege escalation |
| Malicious admin | Has admin role | Abuse elevated access |
| Passive network attacker | Can observe network traffic | Token interception |

---

## STRIDE Analysis

> To be filled after the pentest phase. For each component, assess each STRIDE category.

| Component | Spoofing | Tampering | Repudiation | Info Disclosure | DoS | Elevation of Privilege |
|-----------|----------|-----------|-------------|-----------------|-----|----------------------|
| Auth endpoints | | | | | | |
| Vault CRUD endpoints | | | | | | |
| Sharing feature | | | | | | |
| Admin endpoints | | | | | | |
| Refresh token mechanism | | | | | | |
| AES-256 encryption at rest | | | | | | |
| Frontend (client-side) | | | | | | |

---

## Mitigations

> To be filled as vulnerabilities are found and fixed. Link to relevant [vulnerability](vulnerabilities/) and [fix](fixes/) documents.

| Threat | Mitigation | Status | Reference |
|--------|-----------|--------|-----------|
| | | | |
