# SeoHelper

This package helps you to add meta-tags, sitemap.xml and robots.txt into your project easily.

## Usage

1. First thing first you should install the package via Nuget. 
```sh
Install-Package SeoHelper -Version 1.0.0
```
or

```sh
dotnet add package SeoHelper --version 1.0.0
```

2. Add following service registrations to `ConfigureServices` method in **Startup.cs**.
> Note: You can define your seo options (meta-tags, sitemap.xml and robots.txt) either specifying them in `SeoOptions` class or appsettings.json (under a section. E.g: SeoOptions or SeoConfigurations).

```sh
services.AddSeo(Configuration, sectionName: "MySeoSettings");
```
> If you want to specify your options via appsettings.json. You can create a section named whatever you want ("MySeoSettings" in above usage) and specify it in the related service configuration method (**AddSeo**).

```json
"SeoOptions": {
    "MetaTags": [
      { 
        "RelativeUrl": "/demo",
        "Title": "demo title",
        "Charset": "UTF-8",
        "MetaTagDescriptions": {
          "Author": "author",
          "Description": "description",
          "Keywords": "keywords"
        }
      },
      {
        "RelativeUrl": "/",
        "Title": "index title",
        "Charset": "UTF-8",
        "MetaTagDescriptions": {
          "author": "author",
          "description": "description"
        }
      }
    ],
    "Sitemap": {
      "Urls": [
        {
          "Url": "/article/1",
          "LastModificationDate": "12/12/2012"
        },
        {
          "Url": "/article/2",
          "LastModificationDate": "12/12/2012",
          "Priority": 1.0
        }
      ]
    },
    "RobotsTxt": {
      "DisplaySitemapUrl": true,
      "RobotsTxtSections": [
        {
          "UserAgent": "/google",
          "DisallowUrls": ["/account/manage", "/account/login"],
          "AllowUrls": ["/", "/home-page"]
        },
        {
          "UserAgent": "/yandex",
          "DisallowUrls": ["/account/manage", "/account/login"],
          "AllowUrls": ["*"]
        }
      ]
    }
  },
```

> You can configure your options like below in **appsettings.json**.
