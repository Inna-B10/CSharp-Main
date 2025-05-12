{
  description = "Basic example of TDD in C#.";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
  };

  outputs =
    { self, nixpkgs }:
    let
      withSystem = nixpkgs.lib.genAttrs [
        "x86_64-linux"
        "x86_64-darwin"
        "aarch64-linux"
        "aarch64-darwin"
      ];
      withPkgs =
        callback:
        withSystem (
          system:
          callback (
            import nixpkgs {
              inherit system;
              config.allowUnfree = true;
            }
          )
        );
    in
    {
      devShells = withPkgs (pkgs: {
        default = pkgs.mkShell {
          packages = with pkgs; [
            dotnetCorePackages.sdk_9_0 # Program Development
            hcloud
            terraform
            docker
          ];

          env = {
            DOTNET_ROOT = builtins.toString pkgs.dotnetCorePackages.sdk_9_0;
          };
        };
      });

      formatter = withPkgs (pkgs: pkgs.nixfmt-rfc-style);
    };
}
