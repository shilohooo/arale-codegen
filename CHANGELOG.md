# Change Log

All notable changes to the "relax-your-eyes-green" extension will be documented in this file.

Check [Keep a Changelog](http://keepachangelog.com/) for recommendations on how to structure this file.

## [1.4.1](https://github.com/shilohooo/arale-codegen/releases/tag/v1.4.1) - 2025-09-20

## Fixed

- Fixes a bug (#8) where the right code preview tab does not automatically switch when editing field names in a nested object.

## [1.4.0](https://github.com/shilohooo/arale-codegen/releases/tag/v1.4.0) - 2025-08-12

### Added
- Support for multiple tabs to display generated code lists.
    - Each generated result is displayed in its own tab, making it easier to compare and switch between results.

### Changed
- Refactored internal API response structures to support the multi-tab feature (no impact on external users).
- Improved Monaco Editor state management to preserve cursor and scroll position when switching tabs.


## 1.3.0 - 2025-07-26

### Added

- Support for converting JSON object or array to Spring Data MongoDB entities
- Support for converting JSON object or array to CSharp MongoDB.Driver entities

## 1.2.0 - 2025-04-25

### Added

- Support for converting JSON object or array to JSDoc comment

## 1.1.0 - 2025-03-25

### Added

- Support for convert URL query string to TS type alias or interface
- Support for convert URL query string to JS object literal

## 1.0.0 - 2025-03-22

First release [here](https://github.com/shilohooo/arale-codegen?tab=readme-ov-file#-features)~ 🎉
