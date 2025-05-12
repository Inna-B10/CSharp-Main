resource "hcloud_server" "server" {
  name        = "gitops-server"
  image       = "debian-12"
  server_type = var.server_type
  location    = var.location

  user_data = templatefile("${path.module}/cloud-init.tftpl", {
    ssh_public_key   = var.ssh_public_key
    git_source       = var.git_source
    target_branch    = var.target_branch
    compose_path     = var.compose_path
    refresh_interval = var.update_interval
    hostname         = "gitops-server"
  })

  public_net {
    ipv4_enabled = true
    ipv6_enabled = false
  }
}
