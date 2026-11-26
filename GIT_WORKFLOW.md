# Git workflow

This repository uses a lightweight Git Flow model:

- `master` contains production-ready code. Merge into it only through a reviewed pull request from `develop` for a release.
- `develop` is the integration branch. Feature and fix branches start here and merge back through pull requests.
- Work on a short-lived branch for each change. Use `feature/<issue>-<short-name>` for features, `fix/<issue>-<short-name>` for fixes, and `hotfix/<issue>-<short-name>` for urgent production fixes. Hotfixes branch from `master` and are merged to both `master` and `develop`.
- Do not commit directly to `master` or `develop`. Keep pull requests focused and delete the source branch after merge.

## Commits

Use Conventional Commits with a short imperative summary:

```text
feat(auth): add role assignment
fix(storage): create sqlite database directory
refactor(api): simplify service registration
docs: document local setup
chore(deps): update entity framework packages
```

Use `!` and a `BREAKING CHANGE:` footer for incompatible changes. Keep each commit coherent and avoid committing generated output, local databases, credentials, or IDE-specific state.

## Pull requests

Open feature and fix pull requests against `develop`; open release pull requests from `develop` to `master`. Describe the behavior changed, list verification performed, and call out configuration or database changes. Resolve review comments and ensure the branch is current with its target before merge. Prefer squash merge for short-lived feature branches so the target history stays readable.

## Local configuration

Keep machine-specific values in .NET User Secrets or environment variables. Do not commit real credentials. `appsettings.json` should contain only safe defaults; add a matching `*.example` file when a new local setting needs documentation.

Database files and local secret overrides are excluded by `.gitignore`. If one was already tracked, ignoring it does not remove it from Git; remove it from the index only after confirming it contains no data that must be retained.
