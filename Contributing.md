# Contributing Guide for Phnx Development

This contains a guide for how to setup your computer, and all the tools you'll need to help develop and contribute to `Phnx`

# Tools You'll Need

Some libraries inside this solution require .NET Framework 4.5+ installed on your machine. These are Windows specific, and so can only be developed on a Windows machine. These Windows-only libraries are listen in the [readme](readme.md).

1. Install a [dotnet SDK](https://dotnet.microsoft.com/download).

1. Install [Powershell Core](https://github.com/powershell/powershell#get-powershell) (also known as Powershell 6). This is a cross-platform, open source version of the classic powershell terminal. Most devops tools are built as powershell core scripts.

1. Navigate to the `src` folder. If you're on Windows, run `build_all_core.cmd`. If you're on Linux or OS X, run `build_all_core.sh`.

1. Head to the folder containing the section of the library that you want to contribute to, and open the folder in VS Code (Windows, OS X and Linux), or open the `.sln` file in Visual Studio (Windows only), or open the `.sln` file in Visual Studio for Mac (OS X only).

# Submitting a Change

1. Always create a PR for your change - never directly push your changes to `develop` or `master` branch

1. We generally don't accept PRs that don't have an associated issue. If you think there's a change that needs to happen - submit an issue first! There's a chance that someone's already working on it, or that it's worth reviewing by the team first

1. When you're ready to start work, clone the repository onto your local development environment

1. If there is a `develop` branch, make sure you're on the `develop` branch before you begin. If not, ensure you're on the `master` branch

1. Create a new branch from `develop` with the naming convention of `issue/{ticket #} - {ticket title}` where `ticket #` is replaced with the issue ID, and {ticket title} is replaced by the title of the ticket

1. Make all your changes in this branch

1. Submit a new PR to the `develop` branch with all your changes in. Leave any comments or notes that you want to, if you feel that it would be helpful for us

1. We'll approve or reject and comment on your change. If we reject it - we'll always explain why

# Code of conduct

1. We follow a strict code of conduct across all our solutions. Please read it [here](https://github.com/phoenix-apps/Standards/blob/master/Code_of_Conduct.md) before making any contributions (including comments)

# Most importantly though
From the team at Phoenix Apps Ltd,
Thank you