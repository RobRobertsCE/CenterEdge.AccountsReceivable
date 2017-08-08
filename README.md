# CenterEdge.AccountsReceivable
Hexagonal domain model for Accounts Receivable business logic.

## Building

- Run `.\scripts\Build.ps1 -AutoVersion`
- or, run `.\scripts\Build.ps1 -AutoVersion -VersionSuffix beta001`
- or, run `.\scripts\Build.ps1 -Version 1.1.0-beta001`

## Testing

- Run `.\scripts\Test.ps1`

## Releasing

Create an new release in GitHub, this will trigger a build and release a new NuGet package.  Be sure that the tag name is "release-" followed by the version.  For example, "release-1.1.0" or "release-1.1.0-beta001".
