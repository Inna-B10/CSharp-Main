module "gitops_lite" {
  source = "../../modules/gitops_lite"

  server_type    = "cax21"
  location       = "hel1"
  ssh_public_key = file("id_rsa.pub")

  git_source      = "https://github.com/LarsGJobloop/2025-soa-v2-composition"
  target_branch   = "main"
  compose_path    = "./compose.yaml"
  update_interval = "*/1 * * * *"
}

output "ipv4_address" {
  value = module.gitops_lite.server_ip
}
