resource "hcloud_server" "web_app" {
  name = "web-app"
  server_type = var.instance_type
  image = "debian-12"
  location = var.location

  ssh_keys = [ var.ssh_public_key_id ]

  user_data = templatefile("${path.module}/cloud-init.yaml", {
    application_source = var.application_source
    project_path = var.project_path
    project_name = var.project_name
  })

  public_net {
    ipv4_enabled = true
    ipv6_enabled = false
  }
}
