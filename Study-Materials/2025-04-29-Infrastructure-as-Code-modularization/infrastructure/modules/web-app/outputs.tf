output "server_ip" {
  description = "The IP address of the service"
  value = hcloud_server.web_app.ipv4_address
}
