---
tags: [setup]
---

# Introduction

Welcome to the overview of the API definitions developed during the prototyping phase of the Stichting SEM technology track during the summer of 2020.

For more detailed background and discussion, please visit the functional overview:

[Functional Overview](https://github.com/stichtingsem/functional-overview)

## Services

Each of the API specifications here are connected to specific services:

![services](https://github.com/stichtingsem/functional-overview/raw/master/diagrams/process-diagrams-Services.svg)

The particular mapping of this specification is as follows:

|API Definition| Service Provider | Services Consuming |
|--------------|-------------------|-------------------|
|[Events](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/events.v1.yaml) | all | all |
|[Webhooks](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/events.v1.yaml)| all | all |
|[SIS Data](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/sisdata.v1.yaml) | SIS | MP, LA, LMS |
|[Catalogue Data](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/catalogue.v1.yaml) | LA | MP, LMS |
|[Entitlements](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/entitlement.v1.yaml) | MP | LMS, LA |
|[Usage](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/usage.v1.yaml) | LA | MP, LMS |
|[Progress & Results](https://stichtingsem.stoplight.io/docs/sem-technology-prototype/reference/progress-results.v1.yaml) | LA | MP, LMS |

## A generic architecture

### Reliable and near real time integratiion

A working description of the architecture is contained in this [issue](https://github.com/stichtingsem/technology-prototype/issues/7).

A simple overview of a generic exchange is as follows, note that while this may look complex at first glance, it is a common pattern used in the B2B software space and is straight forward to implement on both sides of the exchange:

![architecture](https://github.com/stichtingsem/technology-prototype/raw/master/docs/event-architecture.svg)

### Secure by design using open standards

The security and data minimization approach is outlined here: [security](https://github.com/stichtingsem/technology-prototype/issues/18)

