{
  description = "Infrastructure as Code (IaC) - Introduction";

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
            terraform
            opentofu
          ];
        };
      });

      formatter = withPkgs (pkgs: pkgs.nixfmt-rfc-style);
    };
}
