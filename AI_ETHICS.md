# Responsible and Ethical AI Use
The eSusFarm project applies AI to support inclusive, transparent, and responsible financial services for smallholder farmers. Our guiding principles are:
## Fairness & Non-Discrimination
- We monitor sex-disaggregated and youth participation metrics.
- We audit models to detect and mitigate gender or socio-economic bias.
- Farmers have an appeal process if they feel decisions are unfair.
## Privacy by Design
- We use **federated learning**: raw farmer data remains local and is not centralized. Only model updates are shared and raw data is discarded after training.
- Data minimization and explicit consent are built into all workflows.
## Explainability
- Each farmer credit/proficiency score includes a plain-language explanation of the top contributing factors.
- Field officers are trained to interpret and communicate these explanations.
## Human Oversight
- Automated triggers are supplemented by human validation (extension officers, agronomists).
- Final financing decisions are reviewable and reversible by humans.
## Do No Harm
- We publish a [Do No Harm](docs/DO_NO_HARM.rst) framework for responsible use.
- We are committed to grievance redress, continuous bias testing, and community co-design.
For concerns or suggestions, contact **admin@esusfarm.africa**.
10:53
ready-to-use Digital Public Goods metadata file (dpg-registry.yml) you can place at the root of either repo (ideally both, API + Web). This is the file the DPG Registry crawler looks for to validate your submission.
version: "1.0"
dpga:
  name: "eSusFarm Blockchain Weather Index Insurance"
  description: >
    An open-source blockchain-powered parametric crop insurance solution
    that provides automated, transparent, and fair payouts to smallholder
    farmers affected by climate risks. It leverages smart contracts on
    Ethereum and federated learning-based farmer scoring to ensure
    equitable insurance access while protecting farmer data privac
  license: "MPL-2.0"
  license_url: "https://www.mozilla.org/en-US/MPL/2.0/"
  repos:
    - "https://github.com/eSusFarm/eSusInsurers-Web"
    - "https://github.com/eSusFarm/eSusInsurers-API"
  documentation:
    - "https://esusinsurers-web.readthedocs.io/en/latest/"
    - "https://esusinsurers-api.readthedocs.io/en/latest/index.html"
  contribution_and_governance:
    - "https://github.com/eSusFarm/eSusInsurers-API/blob/develop/CONTRIBUTING.md"
    - "https://github.com/eSusFarm/eSusInsurers-API/blob/develop/PROJECT_CHARTER.md"
  security:
    - "https://github.com/eSusFarm/eSusInsurers-API/blob/develop/docs/Privacy%20Policy.rst"
    - "https://github.com/eSusFarm/eSusInsurers-API/blob/develop/SECURITY.md"
  evidence_of_use:
    - "https://blog.etherisc.com/etherisc-esusfarm-and-agro-consortium-enter-strategic-crop-insurance-partnership-strengthening-8184165782f5"
CODE_OF_CONDUCT
add link
Security Policy
 add link
Responsible and Ethical AI Use
add link
  sdgs:
    - number: 1
      name: "No Poverty"
    - number: 2
      name: "Zero Hunger"
    - number: 5
      name: "Gender Equality"
    - number: 8
      name: "Decent Work and Economic Growth"
    - number: 13
      name: "Climate Action"
  stage: "DPG Candidate"
  maintainer:
    name: "eSusFarm Africa"
    email: "admin@esusfarm.africa"
    website: "https://esusfarm.africa"
  languages:
    - "Python"
    - "JavaScript"
    - "Node.js"
    - "React"
    - "SQL"
  deployment_examples:
   - "Piloted in South Africa, Uganda, and Eswatini with 300+ farmers accessing blockchain-based crop insurance."
    - "Partnership with Etherisc to integrate decentralized insurance smart contracts for weather-index payouts."
   federated_learning: true
  privacy_by_design: true
  blockchain_smart_contracts: true (edited) 
eSusFarm/eSusInsurers-Web
Language
CSS
Last updated
2 months ago
Added by GitHub
eSusFarm/eSusInsurers-API
Language
C#
Last updated
2 months ago
Added by GitHub
